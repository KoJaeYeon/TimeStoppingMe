/* Simple camera 2 that stays near the “whoToLookAt” target. */

var whoToLookAt : Transform;
var smoothTime : float = 0.3;
private var yVelocity = 0.0;
var distanceAbove: float = 3.0;
var distanceAway = 5.0;

function LateUpdate( ) {
    /* transform the camera so it is a distance away from target */
   transform.position = whoToLookAt.position + Vector3(0, distanceAbove, distanceAway);

    /* Look at the target */
    transform.LookAt( whoToLookAt );
}  /* LateUpdate */