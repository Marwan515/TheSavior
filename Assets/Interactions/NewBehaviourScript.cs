using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
        public class Animal : MonoBehaviour
         {
        public enum AnimalState { Friendly, Aggressive }
        public AnimalState currentState = AnimalState.Friendly;
        private bool isFed = false;

        public void PlayAggressiveAnimation()
        {
            //aggressive animation
            Debug.Log("Playing aggressive animation...");
        }

        public void PlayFeedingAnimation()
        {
            //feeding animation
            Debug.Log("Playing feeding animation...");
        }

        public void Befriend()
        {
            if (!isFed)
            {
                Debug.Log("Animal is now your friend!");
                isFed = true;
            }
        }


        public void AggressiveInteraction()
        {
            if (!isFed)
            {
                PlayAggressiveAnimation();
                Debug.Log("The animal is aggressive! Prepare for a fight!");
           
            }
        }

    }

    public class Interaction : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Animal"))
            {
                Animal animal = other.GetComponent<Animal>();
                if (animal != null)
                {
                    switch (animal.currentState)
                    {
                        case Animal.AnimalState.Friendly:
                            // friendly animation
                            Debug.Log("Press 'F' to feed the animal.");
                            if (Input.GetKeyDown(KeyCode.F))
                                animal.PlayFeedingAnimation();
                            animal.Befriend();
                            break;
                        case Animal.AnimalState.Aggressive:
                            // attacking animation
                            Debug.Log("Animal is aggressive!");
                            animal.PlayAggressiveAnimation();
                            break;
                    }
                }
            }
        }

    }
}
        
