using UnityEngine;

public class Player2 : Player
{
    void Update()
    {
        float horizontalInput = Input.GetAxis("AltHorizontal");
        float verticalInput = Input.GetAxis("AltVertical");
        CalculateMovement(horizontalInput, verticalInput);
        if (Input.GetKeyDown(KeyCode.RightControl) && Time.time > _canFire) FireLaser();
    }
}
