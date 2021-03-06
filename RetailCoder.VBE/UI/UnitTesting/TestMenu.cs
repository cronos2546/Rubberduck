﻿using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Vbe.Interop;
using Rubberduck.Properties;
using Rubberduck.UnitTesting;

namespace Rubberduck.UI.UnitTesting
{
    [ComVisible(false)]
    public class TestMenu : Menu
    {
        // 2743: play icon with stopwatch
        // 3039: module icon || 3119 || 621 || 589 || 472
        // 3170: class module icon

        //private readonly VBE _vbe;
        private readonly TestEngine _engine;

        public TestMenu(VBE vbe, AddIn addInInstance):base(vbe, addInInstance)
        {
            var testExplorer = new TestExplorerWindow();
            var toolWindow = CreateToolWindow("Test Explorer", testExplorer);
            _engine = new TestEngine(vbe, testExplorer, toolWindow);

            //hack: to keep testexplorer from being visible when testmenu is added
            toolWindow.Visible = false;
        }

        private CommandBarButton _runAllTestsButton;
        private CommandBarButton _windowsTestExplorerButton;

        public void Initialize(CommandBarControls menuControls)
        {
            var menu = menuControls.Add(MsoControlType.msoControlPopup, Temporary: true) as CommandBarPopup;
            Debug.Assert(menu != null);

            menu.Caption = "Te&st";

            _windowsTestExplorerButton = AddMenuButton(menu, "&Test Explorer", Resources.TestManager_8590_32);
            _windowsTestExplorerButton.Click += OnTestExplorerButtonClick;

            _runAllTestsButton = AddMenuButton(menu, "&Run All Tests", Resources.AllLoadedTests_8644_24);
            _runAllTestsButton.BeginGroup = true;
            _runAllTestsButton.Click += OnRunAllTestsButtonClick;
        }

        void OnRunAllTestsButtonClick(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            _engine.SynchronizeTests();
            _engine.Run();
        }

        void OnTestExplorerButtonClick(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            _engine.ShowExplorer();
        }

        public new void Dispose()
        {
            _engine.Dispose();
            base.Dispose();
        }
    }
}
