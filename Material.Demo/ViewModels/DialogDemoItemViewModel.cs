﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Material.Demo.ViewModels {
    public class DialogDemoItemViewModel : ViewModelBase {
        private readonly Func<IAsyncEnumerable<string>> _commandHandler;

        private ICommand _command;

        private string _header;

        private string? _result;
        public DialogDemoItemViewModel(string header, Func<IAsyncEnumerable<string>> handler) {
            _header = header;
            _commandHandler = handler;

            _command = new RelayCommand(OnExecuteCommandHandler);
        }

        public ICommand Command {
            get => _command;
            set {
                _command = value;
                OnPropertyChanged();
            }
        }

        public string Header {
            get => _header;
            set {
                _header = value;
                OnPropertyChanged();
            }
        }

        public string? Result {
            get => _result;
            set {
                _result = value;
                OnPropertyChanged();
            }
        }

        private async void OnExecuteCommandHandler(object? arg) {
            Result = "Waiting result...";

            var builder = new StringBuilder();

            await foreach (var str in _commandHandler()) {
                builder.AppendLine(str);
                Result = builder.ToString();
            }
        }
    }
}