﻿using System;
using System.Windows.Input;

namespace FridgeManagerWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// 
/// 
/// 
/// </summary>
///
/// 


public class RelayCommand<T> : ICommand
{
    private readonly Action<T> execute;
    private readonly Func<T, bool> canExecute;

    public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }

    public bool CanExecute(object parameter) => canExecute == null || canExecute((T)parameter);
    public void Execute(object parameter) => execute((T)parameter);
    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}