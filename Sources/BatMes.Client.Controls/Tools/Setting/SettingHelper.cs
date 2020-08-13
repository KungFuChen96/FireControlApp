using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Tools
{
    public static class SettingHelper
    {
        public static void BindModel(this IEnumerable contorls, BaseSettings Model, int sub)
        {
            foreach (Control control in contorls)
            {
                dynamic Value = Model[control.Name.Substring(sub)];//根据控件的 Name 属性索引 Model 对应的属性值
                if (control is CheckBox check)
                    check.Checked = Value;
                else if (control is TextBox)
                    control.Text = Value.ToString();
                else if (control is ComboBox temp)
                    temp.SelectedText = Value;
                else if (control is RadioButton t)
                    t.Checked = Value;
            }
        }


        public static void UpdateModel(this IEnumerable controls, BaseSettings Model, int sub)
        {
            foreach (Control control in controls)
            {
                string propName = control.Name.Substring(sub);
                if (control is CheckBox checkbox)
                    Model[propName] = checkbox.Checked;
                else if (control is TextBox)
                {
                    string text = control.Text.Trim();

                    Type type = Model[propName, null];//获取要设置的属性的类型
                    if (type != typeof(string))
                    {
                        //根据属性类型获取转换器
                        TypeConverter converter = TypeDescriptor.GetConverter(type);
                        Model[propName] = converter.ConvertFromString(text);
                        continue;
                    }
                    Model[propName] = text;
                }
                else if (control is ComboBox comb)
                    Model[propName] = comb.SelectedText;
                else if (control is RadioButton rdo)
                    Model[propName] = rdo.Checked;
            }
        }




    }
}
