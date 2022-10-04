public class ExtendedMag : AttachmentItem
{
    public int ammoAddition
    {
        get
        {
            return (itemInfo as ExtendedMagInfo).bulletsAddition;
        }
    }

    
}
