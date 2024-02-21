using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketTagInteractor : XRSocketInteractor
{
    public string targetTag;
    
    public override bool CanHover(IXRHoverInteractable interactible)
    {
        return base.CanHover(interactible) && interactible.transform.tag == targetTag;
    }

    public override bool CanSelect(IXRSelectInteractable interactible)
    {
        return base.CanSelect(interactible) && interactible.transform.tag == targetTag;
    }
}
