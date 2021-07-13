using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWheel : MonoBehaviour
    {
        public GameObject FRwheel;
        public GameObject FLwheel;
        public GameObject BLwheel;
        public GameObject BRwheel;
        public GameObject divider;
        public GameObject car;
        private int layer_mask;
        private int collider_mask;
        public CommandServer CommandServer;

        private void Start()
        {
            layer_mask = LayerMask.GetMask("road");
            collider_mask = LayerMask.GetMask("divider");
        }

    public void wheelRunner() {
        CommandServer.frWheel = checkWheels(FRwheel);
        CommandServer.flWheel = checkWheels(FLwheel);
        CommandServer.brWheel = checkWheels(BRwheel);
        CommandServer.blWheel = checkWheels(BLwheel);
        CommandServer.divider = dividerCheck(FLwheel);
        ///CommandServer.Collide =  OnTriggerEnter(divider);
    }
        bool checkWheels(GameObject wheel)
        {
            Vector3 fwd = wheel.transform.up * -1;
            if (!Physics.Raycast(wheel.transform.position, fwd, 1, layer_mask))
            {
                return (true);
            }
            return (false);
        }

        bool dividerCheck(GameObject car) {
        Vector3 back = new Vector3(161.5f, -79.62f, -41.49f);
        Vector3 front = new Vector3(212.5f, -79.62f, -41.49f);
        if (car.transform.position.x > back.x & car.transform.position.x < front.x)
        {
            if ((car.transform.position.z - back.z) > 1.5f) {
                return (true);
            }
        }
        return (false);
    }
}
        
