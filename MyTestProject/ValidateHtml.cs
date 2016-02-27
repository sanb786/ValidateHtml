using System;
using System.Collections;
using System.Collections.Generic;

namespace MyTestProject
{
    public class ValidateHtml
    {
        private Stack<string> _htmlTag;
        private string _currentWord = string.Empty;
        private bool _canAdd = false;
        private string _exceptionReport;
        bool _verifyTag = false;
        IList<string> _voidElements;

        public ValidateHtml()
        {
            _htmlTag = new Stack<string>();
            _voidElements = new List<string>() { "area", "base", "br", "col", "embed", "hr", "img", "input", "keygen", "link", "menuitem", "meta", "param", "source", "track", "wbr" };
        }

        /// <summary>
        /// Processes the open tag
        /// Adds opening tags to stack
        /// </summary>
        /// <param name="pointer">The pointer.</param>
        /// <returns></returns>
        public object ProcessOpenTag(IEnumerator pointer)
        {

            while (pointer.MoveNext())
            {
                if (Convert.ToChar(pointer.Current) == '/')// expects closing > verify the tag next
                {
                    pointer.MoveNext();
                    _verifyTag = true;
                }
                if (Convert.ToChar(pointer.Current) == '>')
                {
                    if (_voidElements.Contains(_currentWord))
                        _currentWord = String.Empty;
                    else
                        ProcessCloseTag(_verifyTag);

                    _verifyTag = false;
                    return null;
                }

                if (_canAdd && !char.IsWhiteSpace(Convert.ToChar(pointer.Current))) _currentWord += pointer.Current; // create the current tag.
                if (_canAdd && !string.IsNullOrWhiteSpace(_currentWord) && char.IsWhiteSpace(Convert.ToChar(pointer.Current)))
                    _canAdd = false;
                return ProcessOpenTag(pointer);
            }
            return null;
        }


        /// <summary>
        /// Processes the close tag.
        /// Removes valid tags from stack
        /// Remaining  items are unmatched
        /// </summary>
        /// <param name="verifyTag">if set to <c>true</c> [verify tag].</param>
        /// <returns></returns>
        public object ProcessCloseTag(bool verifyTag)
        {
            if (!verifyTag)
            {
                _htmlTag.Push(_currentWord);
                _currentWord = string.Empty;
                return null;
            }
            else if (_htmlTag.Count > 0)
            {
                if (Convert.ToBoolean(string.Compare(_htmlTag.Peek(), _currentWord, StringComparison.CurrentCultureIgnoreCase) == 0))
                {
                    _htmlTag.Pop();
                    _currentWord = string.Empty;
                    return null;
                }
                else
                {
                    _exceptionReport += $"Expected {_htmlTag.Pop()} Found {_currentWord} \n";
                    ProcessCloseTag(verifyTag);
                }
            }
            else
            {
                _exceptionReport += $"Expected Starting tag for {_currentWord} \n";
                _currentWord = string.Empty;
            }

            return null;
        }


        public void GetExceptionReport()
        {
            foreach (var item in _htmlTag)
                _exceptionReport += $"Expected Ending tag for {item} \n";

            if (string.IsNullOrWhiteSpace(_exceptionReport)) _exceptionReport = "Correctly tagged paragraph";
            Console.WriteLine(_exceptionReport);
        }


        public bool CheckElement(char[] elementArray)
        {
            var enumerableArray = elementArray.GetEnumerator();
            while (enumerableArray.MoveNext())
            {
                var currentChar = enumerableArray.Current;
                if (Convert.ToChar(currentChar) == '<') //start tag
                {
                    _canAdd = true;
                    ProcessOpenTag(enumerableArray);
                }
            }
            return true;
        }
    }
}
