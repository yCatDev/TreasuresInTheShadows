using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskPriorityEnum
{
    Default,
    High,
    Interrupt
}
public interface ITask
{
    TaskPriorityEnum Priority { get; }

    void Start();
    ITask Subscribe(Action feedback);
    void Stop();
}

public class Task : ITask
{
    public TaskPriorityEnum Priority
    {
        get
        {
            return _taskPriority;
        }
    }

    private TaskPriorityEnum _taskPriority = TaskPriorityEnum.Default;

    private Action _feedback;
    private MonoBehaviour _coroutineHost;
    private Coroutine _coroutine;
    private IEnumerator _taskAction;

    public static Task Create(IEnumerator taskAction, MonoBehaviour behaviour, TaskPriorityEnum priority = TaskPriorityEnum.Default)
    {
        return new Task(taskAction, behaviour,priority);
    }

    public Task(IEnumerator taskAction, MonoBehaviour behaviour, TaskPriorityEnum priority = TaskPriorityEnum.Default)
    {
        _coroutineHost = behaviour;
        _taskPriority = priority;
        _taskAction = taskAction;
    }

    public void Start()
    {
        if (_coroutine == null)
        {
            _coroutine = _coroutineHost.StartCoroutine(RunTask());
        }
    }

    public void Stop()
    {
        if (_coroutine != null)
        {
            _coroutineHost.StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    public ITask Subscribe(Action feedback)
    {
        _feedback += feedback;

        return this;
    }


    private IEnumerator RunTask()
    {
        yield return _taskAction;

        CallSubscribe();
    }

    private void CallSubscribe()
    {
        if (_feedback != null)
        {
            _feedback();
        }
    }
}

public class TaskManager
{
    public ITask CurrentTask
    {
        get
        {
            return _currentTask;
        }
    }

    private ITask _currentTask;
    private List<ITask> _tasks = new List<ITask>();
    private MonoBehaviour _coroutineHost;

    public TaskManager(MonoBehaviour mb)
    {
        _coroutineHost = mb;
    }

    public void AddTask(IEnumerator taskAction, Action callback, TaskPriorityEnum taskPriority = TaskPriorityEnum.Default)
    {
        var task = Task.Create(taskAction, _coroutineHost, taskPriority).Subscribe(callback);

        ProcessingAddedTask(task, taskPriority);
    }

    public void Break()
    {
        if (_currentTask != null)
        {
            _currentTask.Stop();
        }
    }

    public void Restore()
    {
        TaskQueueProcessing();
    }

    public void Clear()
    {
        Break();

        _tasks.Clear();
    }

    private void ProcessingAddedTask(ITask task, TaskPriorityEnum taskPriority)
    {
        switch (taskPriority)
        {
            case TaskPriorityEnum.Default:
                {
                    _tasks.Add(task);
                }
                break;
            case TaskPriorityEnum.High:
                {
                    _tasks.Insert(0, task);
                }
                break;

                return;
            case TaskPriorityEnum.Interrupt:
                {
                    if (_currentTask != null && _currentTask.Priority != TaskPriorityEnum.Interrupt)
                    {
                        _currentTask.Stop();
                    }

                    _currentTask = task;

                    task.Subscribe(TaskQueueProcessing).Start();
                }
                break;
        }

        if (_currentTask == null)
        {
            _currentTask = GetNextTask();

            if (_currentTask != null)
            {
                _currentTask.Subscribe(TaskQueueProcessing).Start();
            }
        }
    }

    private void TaskQueueProcessing()
    {
        _currentTask = GetNextTask();

        if (_currentTask != null)
        {
            _currentTask.Subscribe(TaskQueueProcessing).Start();
        }
    }

    private ITask GetNextTask()
    {
        if (_tasks.Count > 0)
        {
            var returnValue = _tasks[0]; _tasks.RemoveAt(0);

            return returnValue;
        }
        else
        {
            return null;
        }
    }
}