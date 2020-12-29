﻿using System.Windows.Input;
using Macad.Core;

namespace Macad.Window
{
    public class MainWindowModel : ViewModelBase
    {
        #region Properties

        public AppContext Context
        {
            get { return _Context; }
            private set
            {
                _Context = value;
                RaisePropertyChanged();
            }
        }

        //--------------------------------------------------------------------------------------------------

        #endregion

        #region Member

        AppContext _Context;

        public ViewportViewModel ViewportViewModel { get; private set; }

        //--------------------------------------------------------------------------------------------------

        #endregion

        public MainWindowModel()
        {
            Context = AppContext.Current;

            Context.MessageHandler.ProgressMessage += _MessageHandler_ProgressMessage;

            ViewportViewModel = new ViewportViewModel();

            AppCommands.InitApplication.Execute();
        }

        //--------------------------------------------------------------------------------------------------

        void _MessageHandler_ProgressMessage(object sender, MessageHandler.ProgressMessageEventArgs e)
        {
            switch (e.Reason)
            {
                case MessageHandler.ProgressMessageEventReason.ProcessingStarted:
                    Mouse.OverrideCursor = Interaction.Cursors.Wait;
                    break;
                case MessageHandler.ProgressMessageEventReason.ProcessingStopped:
                    Mouse.OverrideCursor = null;
                    break;
            }
        }

        //--------------------------------------------------------------------------------------------------

        public bool GlobalKeyDown(KeyEventArgs keyEventArgs)
        {
            return AppContext.Current.ShortcutHandler.KeyPressed(ShortcutScope.Application, keyEventArgs.Key, Keyboard.Modifiers);
        }

        //--------------------------------------------------------------------------------------------------

    }
}