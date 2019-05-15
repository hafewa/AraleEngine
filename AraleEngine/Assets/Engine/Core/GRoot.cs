﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Arale.Engine
{
    public abstract class GRoot : MonoBehaviour
    {
        public const string EventSceneLoad = "Game.SceneLoad";
        public const string EventGameFocus = "Game.Focus";
        public static GRoot single;
        public int mLaunchFlag;
        public bool mUseLua;
        public Log.Tag mLogTag;
        public Log.Type mLogLevel;
        public string mGameServer="127.0.0.1:80";
        public string mResServer="http://127.0.0.1:8080/update/";
        [System.NonSerialized]
        public GDevice mDevice;

        List<VoidDelegate> mUpdates = new List<VoidDelegate>();
        void Awake()
        {
            single = this;
            Log.init ();
            Log.mFilter = (int)mLogTag;
            Log.mDebugLevel = (int)mLogLevel;
                
            mDevice = new GDevice ();
            DontDestroyOnLoad (this);   
        }

        void Start ()
        {
            if(mUseLua)gameObject.AddComponent<LuaRoot>();
            Application.targetFrameRate = 60;
            Application.runInBackground = true;
            if (EventSystem.current != null)
            {
                DontDestroyOnLoad(EventSystem.current.gameObject);
            }
            ResLoad.init(this);
            gameStart();
        }

        void Update()
        {
            RTime.R.Update();
            gameUpdate();
            for (int i = mUpdates.Count - 1; i >= 0; --i)
            {
                mUpdates[i]();
            }
        }

        void OnDestroy ()
        {
            gameExit();
            ResLoad.deinit();
            Log.deinit ();
        }

        void OnLevelWasLoaded(int level)
        {
            WindowMgr.single.CloseAllWindow();
            string name = SceneManager.GetActiveScene().name;
            EventMgr.single.SendEvent(EventSceneLoad, name);
        }

        void OnApplicationFocus(bool isFocus)
        {
            EventMgr.single.SendEvent(EventGameFocus, isFocus);
        }

        protected abstract void gameStart();
        protected abstract void gameExit();
        protected abstract void gameUpdate();
        public void AddUpdate(VoidDelegate updateFunc)
        {
            mUpdates.Insert(0, updateFunc);
        }

        public void RemoveUpdate(VoidDelegate updateFunc)
        {
            mUpdates.Remove(updateFunc);
        }
    }
}