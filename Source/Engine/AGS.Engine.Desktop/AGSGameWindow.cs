﻿using System;
using AGS.API;
using Autofac;
using OpenTK;
using OpenTK.Graphics;

namespace AGS.Engine.Desktop
{
	public class AGSGameWindow : IGameWindow
	{
        private GameWindow _gameWindow;
        private IGameWindowSize _windowSize;
        private FrameEventArgs _updateFrameArgs, _renderFrameArgs;

        public AGSGameWindow(IGameSettings settings, IGameWindowSize windowSize, Resolver resolver)
        {
            _windowSize = windowSize;
            _gameWindow = new GameWindow(settings.WindowSize.Width, settings.WindowSize.Height, 
                                         GraphicsMode.Default, settings.Title);
            
            var updater = new ContainerBuilder();
            updater.RegisterType<AGSInput>().SingleInstance().As<IInput>();
            updater.RegisterInstance(_gameWindow);
            updater.Update(resolver.Container);

            _updateFrameArgs = new FrameEventArgs();
            _renderFrameArgs = new FrameEventArgs();
            _gameWindow.UpdateFrame += onUpdateFrame;
            _gameWindow.RenderFrame += onRenderFrame;
        }

        public event EventHandler<EventArgs> Load
        {
            add { _gameWindow.Load += value; }
            remove { _gameWindow.Load -= value; }
        }
        public event EventHandler<EventArgs> Resize
        {
            add { _gameWindow.Resize += value; }
            remove { _gameWindow.Resize -= value; }
        }
        public event EventHandler<FrameEventArgs> UpdateFrame;
        public event EventHandler<FrameEventArgs> RenderFrame;
         
        public double TargetUpdateFrequency { get => _gameWindow.TargetUpdateFrequency; set => _gameWindow.TargetUpdateFrequency = value; }
        public string Title { get => _gameWindow.Title; set => _gameWindow.Title = value; }
        public VsyncMode Vsync { get => (VsyncMode)_gameWindow.VSync; set => _gameWindow.VSync = (VSyncMode)value; }
        public bool IsExiting => _gameWindow.IsExiting;
        public API.WindowState WindowState
        {
            get => (API.WindowState)_gameWindow.WindowState;
            set => _gameWindow.WindowState = (OpenTK.WindowState)value;
        }
        public API.WindowBorder WindowBorder
        {
            get => (API.WindowBorder)_gameWindow.WindowBorder;
            set => _gameWindow.WindowBorder = (OpenTK.WindowBorder)value;
        }
        public int Width => _gameWindow.Width;
        public int Height => _gameWindow.Height;
        public int ClientWidth => _windowSize.GetWidth(_gameWindow);
        public int ClientHeight => _windowSize.GetHeight(_gameWindow);
        public void SetSize(Size size) => _windowSize.SetSize(_gameWindow, size);

        public void Run(double updateRate) => _gameWindow.Run(updateRate);
        public void SwapBuffers() => _gameWindow.SwapBuffers();
        public void Exit() => _gameWindow.Exit();
        public void Dispose() => _gameWindow.Dispose();

        private void onUpdateFrame(object sender, OpenTK.FrameEventArgs args)
        {
            _updateFrameArgs.Time = args.Time;
            UpdateFrame?.Invoke(sender, _updateFrameArgs);
        }

        private void onRenderFrame(object sender, OpenTK.FrameEventArgs args)
        {
            _renderFrameArgs.Time = args.Time;
            RenderFrame?.Invoke(sender, _renderFrameArgs);
        }
    }
}

