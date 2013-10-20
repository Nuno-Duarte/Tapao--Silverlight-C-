using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace TapaoNovo
{
    public class KeyManager
    {
        //HashSet<Key> keys = new HashSet<Key>();
        List<Key> keys = new List<Key>();
        public void Add(Key key)
        {
            keys.Add(key);
        }

        public void Remove(Key key)
        {
            keys.Remove(key);
        }

        public bool isPressed(Key key)
        {
            return keys.Contains(key);
        }
    }
}
