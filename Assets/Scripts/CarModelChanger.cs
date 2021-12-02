using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarModelChanger : MonoBehaviour
{
    [SerializeField] GameObject []carModels;
    public int currentCar;

    // Start is called before the first frame update
    void Start()
    {
        if(carModels.Length < 1 ) { Debug.LogWarning("Assign MeshModels to Car"); } //Check if there are models assigned.
        currentCar = FindObjectOfType<GameManager>().chosenCar; //Starting Model 
        carModels[currentCar].SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void assignNextCarModel() //Takes the list of CarModels set the current model Inactive changed to next model on the list and activates it. models should be on the car model.
    {

        carModels[currentCar].SetActive(false);
        currentCar++;
        
        if (currentCar == carModels.Length)
        {
            currentCar = 0;
            carModels[currentCar].SetActive(true);
        }
        carModels[currentCar].SetActive(true);

    }
}
