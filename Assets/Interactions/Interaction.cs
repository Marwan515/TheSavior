using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Interaction : MonoBehaviour
{
    private Animal currentAnimal;
    private MainCharacter character;
    private UI ui;

    public Animal CurrentAnimal => currentAnimal;


    private void Start()
    {
        ui = FindAnyObjectByType<UI>();
        character = FindObjectOfType<MainCharacter>();

        if (character == null)
        {
            Debug.LogError("MainCharacter not found in the scene.");
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        
        if (collider.CompareTag("Animal"))
        {
            Animal animal = collider.GetComponent<Animal>();
            if (animal != null)
            {
                currentAnimal = animal;
                switch (animal.currentState)
                {
                    case Animal.AnimalState.Friendly:
                        currentAnimal.PlayAggressiveAnimation();
                        Debug.Log("Press 'F' to feed the animal.");                      
                        break;
                    case Animal.AnimalState.Aggressive:
                        Debug.Log("Animal is aggressive!");
                        StartCoroutine(HandleAggressiveInteraction(animal));
                        break;
                }
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.CompareTag("Animal"))
        {
            currentAnimal = null;
            StopAllCoroutines();
        }
    }

    private void Update()
    {
        if (currentAnimal != null && currentAnimal.currentState == Animal.AnimalState.Friendly)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (character != null)
                {
                    character.PlayFeedingAnimation();
                    currentAnimal.PlayEatingAnimation();
                    ui.GainXP(50);
                    StartCoroutine(FeedAnimal());

                }
                else
                {
                    Debug.LogError("Character is not found.");
                }
            }
        }
    }

    private IEnumerator HandleAggressiveInteraction(Animal animal)
    {
        animal.PlayAggressiveAnimation();
        while (currentAnimal == animal && currentAnimal.currentState == Animal.AnimalState.Aggressive)
        {
            yield return new WaitForSeconds(2f);
            ui.TakeDamage(50);
        }
    }


    private IEnumerator FeedAnimal()
    {
        if (currentAnimal != null)
        {
            // Play eating animation on the animal
            currentAnimal.PlayEatingAnimation();

            // Wait for the eating animation to complete
            yield return new WaitForSeconds(currentAnimal.eatingAnimationLength);

            // Play friendly animation after eating animation
            currentAnimal.PlayFriendlyAnimation();
            currentAnimal.Befriend();
            Debug.Log("Animal is now your friend!");
        }
    }



}
