﻿using System;
using System.Collections.Generic;

public enum Priority
{
    Low = 1,
    Medium = 2,
    High = 3
}

public enum NotificationType
{
    Info = 1,
    Warning = 2,
    Error = 3
}


public class User
{
    public string Name { get; }
    public Priority PriorityLevel { get; }
    public User(string name, Priority priority)
    {
        Name = name;
        PriorityLevel = priority;
    }
    public void ReceiveNotification(NotificationType type, string message)
    {
        Console.WriteLine($"{Name} получает уведомление ({type}): {message}");
    }
}


public class NotificationSystem
{
    private List<Action<string>> infoSubscribers = new();
    private List<Action<string>> warningSubscribers = new();
    private List<Action<string>> errorSubscribers = new();
    public void Subscribe(User user, NotificationType type)
    {
        Action<string> action = message => user.ReceiveNotification(type, message);
        switch (type)
        {
            case NotificationType.Info:
                infoSubscribers.Add(action);
                break;
            case NotificationType.Warning:
                warningSubscribers.Add(action);
                break;
            case NotificationType.Error:
                errorSubscribers.Add(action);
                break;
        }
    }
    public void SendNotification(NotificationType type, Priority priority, string message)
    {
        Console.WriteLine($"\nОтправка {type} уведомления с приоритетом {priority}: {message}");
        List<Action<string>> subscribers;
        switch (type)
        {
            case NotificationType.Info:
                subscribers = infoSubscribers;
                break;
            case NotificationType.Warning:
                subscribers = warningSubscribers;
                break;
            case NotificationType.Error:
                subscribers = errorSubscribers;
                break;
            default:
                throw new ArgumentException("Неизвестный тип уведомления");
        }
        foreach (var subscriber in subscribers)
        {
            subscriber(message);
        }
    }
}



class Program
{
    delegate int[] CountDelegate(int[] arr);
    static void Main()
    {
        Console.WriteLine("HARD TASK");
        NotificationSystem system = new NotificationSystem();
        User alice = new User("A", Priority.Low);
        User bob = new User("Bob", Priority.Medium);
        User charlie = new User("Charlie", Priority.High);
        system.Subscribe(alice, NotificationType.Info);
        system.Subscribe(bob, NotificationType.Warning);
        system.Subscribe(charlie, NotificationType.Error);
        system.Subscribe(charlie, NotificationType.Warning);
        system.SendNotification(NotificationType.Info, Priority.Low, "Info message");
        system.SendNotification(NotificationType.Warning, Priority.Medium, "Warn message");
        system.SendNotification(NotificationType.Error, Priority.High, "Error message");
        Console.WriteLine("\nBASIC TASK");
        int[] numbers = { -3, 5, 0, -1, 4, 0, -2, 7 };
        CountDelegate countNumbers = delegate (int[] arr)
        {
            int negativeCount = 0, positiveCount = 0, zeroCount = 0;
            foreach (int num in arr)
            {
                if (num < 0) negativeCount++;
                else if (num > 0) positiveCount++;
                else zeroCount++;
            }
            return new int[] { negativeCount, positiveCount, zeroCount };
        };
        int[] result = countNumbers(numbers);
        Console.WriteLine($"Negative: {result[0]}, Positive: {result[1]}, Zero: {result[2]}");
    }
}

