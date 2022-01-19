using System;
using System.Collections.Generic;
using Prism.Mvvm;
using Prism.Commands;
using System.Text.RegularExpressions;
using System.Data;

namespace HomeworkMVVM.VeiwModels
{
    public class ShellViewModel: BindableBase
    {
        public ShellViewModel()
        {

        }
        
        //Создаем свойство, которое будет привязываться к textblock
        private string _result;
        public string Result
        {
            get { return _result; }
            set { SetProperty(ref _result, value);}
        }
        
        //Создаем свойство, которое будет привязываться к textbox
        private string _expression;
        public string Expression
        {
            get { return _expression; }
            set
            {
                SetProperty(ref _expression, value);
                CalculateCommand.RaiseCanExecuteChanged();
            }
        }

        //Создаем команду, с помощью которой будем вводить цифру или операнд по щелчку на кнопку
        //и эту команду привяжем к значению свойства кнопки Command в стиле кнопки
        // Мы передаем содержание кнопки как параметр, поэтому создаем параметризированную команду
        private DelegateCommand<string>_clickCommand;
        public DelegateCommand<string> ClickCommand =>
            _clickCommand ?? (_clickCommand = new DelegateCommand<string>(ExecuteClickCommand));

        // Создаем команду, производящую расчет выражения
        //и эту команду привяжем к кнопке "="
        private DelegateCommand _calculateCommand;
        public DelegateCommand CalculateCommand =>
           _calculateCommand ?? (_calculateCommand = new DelegateCommand(Calculate,CanCalculate));

        //Метод на основе метода Compute класса DataTable
        // Этот метод присваивает свойству Result вычисленное заданое выражение,  
        // приводит в строковое представление и отчищает выражение
        private void Calculate()
        {
            Result = Expression + "=" + new DataTable().Compute(Expression, "").ToString();
            Expression = string.Empty;
        }
        
        //В этом методе описываем логику проверки выражения
        //Кнопка "=" работает только когда выражение действительное
        private bool CanCalculate()
        {
            var isvalidEspression = Expression != null ? Regex.IsMatch(Expression, @"^\d*\.?\d+(\s*[+*/-]\s*\d*\.?\d+)+$"):false ;
            return isvalidEspression;
        }
        // Этот метод присваивает свойству Expression  значения, которые  отображаются в textbox
        private void ExecuteClickCommand(string p)
        {
           Expression +=p;
        }
    }
}
