using UnityEngine;

public class Player1 : Player
{
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        CalculateMovement(horizontalInput, verticalInput);
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire) FireLaser();
    }
}
