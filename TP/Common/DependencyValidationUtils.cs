using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace TP.Common
{
    public class DependencyValidationUtils
    {
        public static bool IsValid(DependencyObject parent)
        {
            // Validate all the bindings on the parent
            bool valid = true;
            LocalValueEnumerator localValues = parent.GetLocalValueEnumerator();
            while (localValues.MoveNext())
            {
                LocalValueEntry entry = localValues.Current;
                if (BindingOperations.IsDataBound(parent, entry.Property))
                {
                    Binding binding = BindingOperations.GetBinding(parent, entry.Property);
                    if (binding.ValidationRules.Count > 0)
                    {
                        BindingExpression expression = BindingOperations.GetBindingExpression(parent, entry.Property);
                        expression.UpdateSource();

                        if (expression.HasError)
                        {
                            valid = false;
                        }
                    }
                }
            }

            // Validate all the bindings on the children
            System.Collections.IEnumerable children = LogicalTreeHelper.GetChildren(parent);
            foreach (object obj in children)
            {
                if (obj is DependencyObject)
                {
                    DependencyObject child = (DependencyObject)obj;
                    if (!IsValid(child)) { valid = false; }
                }
            }
            return valid;
        }
    }
}
