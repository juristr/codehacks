//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace ClientConsoleApp
//{
//    public interface IToolWindow
//    {
//        string Name { get; set; }
//    }

//    public interface IMenuService
//    {
//        IMenu GetMenu(string menuName);
//    }

//    public interface IMenu
//    {
//        string Name { get; set; }
//        string Title { get; set; }
//        string Toolwindow { get; set; }
//        IEnumerable<IMenuItem> Items { get; set; }

//        IMenuItem GetItem(string itemName);
//    }

//    public interface IMenuItem
//    {
//        string Name { get; set; }
//        string Title { get; set; }
//        Action Handler { get; set; }
//    }

//    public interface IMenuItem<T> : IMenuItem {}
    
//}
