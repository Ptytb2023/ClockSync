using System;
using UnityEngine;
using Infrastructure.Services.Times;
using Zenject;

public abstract class BaseClock : MonoBehaviour
{
    protected ITimeService TimeService;

    [Inject]
    public void Construct(ITimeService timeService) =>
        TimeService = timeService;

    protected virtual void OnEnable() =>
    TimeService.TimeUpdated += OnUpdateTime;

    protected virtual void OnDisable() =>
        TimeService.TimeUpdated -= OnUpdateTime;

    protected abstract void OnUpdateTime(DateTime time);
}
