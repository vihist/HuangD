using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq.Expressions;

using UnityEngine;

using XLua;

using System.Linq;

public partial class MyGame
{
    public class Zhouj
    {
        public enum ENUM_ZHOUJ
        {
            ZHOUJ1,
            ZHOUJ2,
            ZHOUJ3,
            ZHOUJ4,
            ZHOUJ5,
            ZHOUJ6,
            ZHOUJ7,
            ZHOUJ8,
            ZHOUJ9
        }

        public Zhouj(string name)
        {
            _name = name;
        }

        public string name
        {
            get
            {
                return _name;
            }
        }

        private string _name;
    }

    public class ZhoujManager
    {
        public ZhoujManager()
        {
        }
    }

}

