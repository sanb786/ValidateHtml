using System;
using System.Collections.Generic;

namespace MyTestProject
{
    public class FlatternJaggedArray
    {
        List<int> _result;

        public int[] Result
        {
            get
            {
                return _result.ToArray();
            }
        }
       
        public FlatternJaggedArray()
        {
            _result = new List<int>();
        }

        public void FlatternNested(object[] jArray)
        {
            if (jArray == null) return;
            foreach(var item in jArray)
            {
                switch (item.GetType().Name)
                {
                    case "Int32":
                        _result.Add(Convert.ToInt32(item));
                        break;

                    case "Int32[]":
                        FlatternNested(ConvertFromIntArr((int[])item));
                        break;

                    case "Object[]":
                        FlatternNested((object[])item);
                           break;
                }
            }
        }

        private object[] ConvertFromIntArr(int[] intArr)
        {
            var arrLength = intArr.Length;
            if (arrLength == 0) return null;
           
            var ar = new object[arrLength];
            for (int i = 0; i < arrLength; i++)
            {
                ar[i] = intArr[i];
            }
            return ar;
        }
    }
}
