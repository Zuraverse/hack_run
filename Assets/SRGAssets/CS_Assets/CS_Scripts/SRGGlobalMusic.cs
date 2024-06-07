﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using SpeederRunGame;
    /// <summary>
    /// handles a global music source which carries over from scene to scene without resetting the music track.
    /// You can have this script attached to a music object and include that object in each scene, and the script will keep
    /// only the oldest music source in the scene.
    /// </summary>
    public class SRGGlobalMusic:MonoBehaviour 
	{
		[Tooltip("The tag of the music source")]
		public string musicTag = "Music";
		bool CheckOncePerFrame = true;
		
		//The time this instance of the music source has been in the game
		internal float instanceTime = 0;

        private AudioSource audioSource;
        // Music clips for each scene
        public AudioClip music1;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// Awake is used to initialize any variables or game state before the game starts. Awake is called only once during the 
        /// lifetime of the script instance. Awake is called after all objects are initialized so you can safely speak to other 
        /// objects or query them using eg. GameObject.FindWithTag. Each GameObject's Awake is called in a random order between objects. 
        /// Because of this, you should use Awake to set up references between scripts, and use Start to pass any information back and forth. 
        /// Awake is always called before any Start functions. This allows you to order initialization of scripts. Awake can not act as a coroutine.
        /// </summary>
        void  Awake()
		{
			//Find all the music objects in the scene
			GameObject[] musicObjects = GameObject.FindGameObjectsWithTag(musicTag);
            //Keep only the music object which has been in the game for more than 0 seconds
            if ( musicObjects.Length > 1 )
			{
				foreach( var musicObject in musicObjects )
				{
					if ( musicObject.GetComponent<SRGGlobalMusic>() && musicObject.GetComponent<SRGGlobalMusic>().instanceTime <= 0 )    Destroy(gameObject);
				}
			}

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

		/// <summary>
		/// Start is only called once in the lifetime of the behaviour.
		/// The difference between Awake and Start is that Start is only called if the script instance is enabled.
		/// This allows you to delay any initialization code, until it is really needed.
		/// Awake is always called before any Start functions.
		/// This allows you to order initialization of scripts
		/// </summary>
		void  Start()
		{
			//Don't destroy this object when loading a new scene
			DontDestroyOnLoad(transform.gameObject);
		}

        private void Update()
        {
            

			if (CheckOncePerFrame == true)
			{
                // Get the name of the current scene
                string sceneName = SceneManager.GetActiveScene().name;

                        if (sceneName == "SRGStage01" || sceneName == "SRGStage02" || sceneName == "SRGStage03")
                        {
                            audioSource.clip = music1;
                            audioSource.Play();
                            audioSource.volume = 0.2f;
                            CheckOncePerFrame = false;
                        }
            }

            // Add more conditions here for other scenes and music clips
        }

    }
