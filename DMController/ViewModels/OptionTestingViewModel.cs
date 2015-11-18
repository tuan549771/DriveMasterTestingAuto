using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DMController.ViewModels
{
    class OptionTestingViewModel : IDataErrorInfo
    {
        private string _optionTitle;
        private string _optionName;
        private List<ModeTestingViewModel> _modeValues;
        private int _selectedModeDefaultValue;
        private string _typeOption;
        private string _modeValueTime;

        public string OptionTitle
        {
            get { return _optionTitle; }
            set { _optionTitle = value; }
        }

        public string OptionName
        {
            get { return _optionName; }
            set { _optionName = value; }
        }

        public List<ModeTestingViewModel> ModeValues
        {
            get { return _modeValues; }
            set { _modeValues = value; }
        }

        public int SelectedModeDefaultValue
        {
            get { return _selectedModeDefaultValue; }
            set { _selectedModeDefaultValue = value; }
        }

        public string TypeOption
        {
            get { return _typeOption; }
            set { _typeOption = value; }
        }

        public string ModeValueTime
        {
            get
            {
                _modeValueTime = ModeValues[0].ModeValueAttribute;
                return _modeValueTime;
            }
            set
            {
                _modeValueTime = value;
                if (string.Empty != _modeValueTime)
                    ModeValues[0].ModeValueAttribute = _modeValueTime;
            }
        }

        public OptionTestingViewModel()
        {
            OptionTitle = string.Empty;
            OptionName = string.Empty;
            ModeValues = new List<ModeTestingViewModel>();
            SelectedModeDefaultValue = 0;
            TypeOption = string.Empty;
            ModeValueTime = string.Empty;
        }

        string IDataErrorInfo.Error
        {
            get { return null; }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "ModeValueTime":

                        if (!ModeValueTime.All(Char.IsDigit) || '0' == (ModeValueTime[0]))
                            error = "Please enter a number more than zero( > 0)";
                        break;

                    default:
                        throw new Exception("Unexpected property: " + columnName);
                }
                return error;
            }
        }
    }
}
