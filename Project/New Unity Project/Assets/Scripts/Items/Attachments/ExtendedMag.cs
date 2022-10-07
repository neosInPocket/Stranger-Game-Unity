public class ExtendedMag : AttachmentItem
{
    public float ammoMultiplier
    {
        get
        {
            return (itemInfo as ExtendedMagInfo).ammoMultiplier;
        }
    }

    
}
