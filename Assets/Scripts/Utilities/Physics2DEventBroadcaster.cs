using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public class Physics2DEventBroadcaster : MonoBehaviour
    {
        public Physics2DTriggerEvent onTriggerEnter = new Physics2DTriggerEvent();
        public Physics2DTriggerEvent onTriggerExit = new Physics2DTriggerEvent();
        public Physics2DTriggerEvent onTriggerStay = new Physics2DTriggerEvent();

        public Physics2DCollisionEvent onCollisionEnter = new Physics2DCollisionEvent();
        public Physics2DCollisionEvent onCollisionExit = new Physics2DCollisionEvent();
        public Physics2DCollisionEvent onCollisionStay = new Physics2DCollisionEvent();


        public void OnTriggerEnter2D(Collider2D other) => onTriggerEnter.Invoke(other);
        public void OnTriggerExit2D(Collider2D other) => onTriggerExit.Invoke(other);
        public void OnTriggerStay2D(Collider2D other) => onTriggerStay.Invoke(other);

        public void OnCollisionEnter2D(Collision2D coll) => onCollisionEnter.Invoke(coll);
        public void OnCollisionExit2D(Collision2D coll) => onCollisionExit.Invoke(coll);
        public void OnCollisionStay2D(Collision2D coll) => onCollisionStay.Invoke(coll);
    }

    public class Physics2DTriggerEvent : UnityEvent<Collider2D>
    {
    }

    public class Physics2DCollisionEvent : UnityEvent<Collision2D>
    {
    }
}