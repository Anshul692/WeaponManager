using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject[] Guns;
    [SerializeField] GameObject ActiveWeapon;

    [SerializeField] GameObject[] BarrelAttachments;
    [SerializeField] GameObject[] UnderBarrelAttachments;
    [SerializeField] GameObject[] SightAttachments;

    [SerializeField] Transform GunSpawn;
    [SerializeField] Transform GunAssets;

    [SerializeField] Transform LoadAttachment;

    [SerializeField] GameObject ActiveWeaponBarrelAttachment;
    [SerializeField] GameObject ActiveWeaponUnderBarrelAttachment;
    [SerializeField] GameObject ActiveWeaponSightCursor;

    [SerializeField] int ActiveWeaponCursor = 0;



    void Start()
    {

        LoadGunAssets();
        LoadGun(Guns);
        
    }
    void LoadGun(GameObject[] Guns)
    {

        ActiveWeapon = Guns[0];
        ActiveWeapon = Instantiate(ActiveWeapon, GunSpawn);
        AttachmentManager();

    }
    void SetActiveDisableAttachments()
    {
        LoadAttachment = ActiveWeapon.transform.GetChild(0);
        LoadAttachment = LoadAttachment.Find("Barrel");
        for (int x = 0; x < LoadAttachment.childCount ; x++)
        {
            LoadAttachment.GetChild(x).gameObject.SetActive(false);
        }

        LoadAttachment = ActiveWeapon.transform.GetChild(0);
        LoadAttachment = LoadAttachment.Find("UnderBarrel");
        for (int x = 0; x < LoadAttachment.childCount; x++)
        {
            LoadAttachment.GetChild(x).gameObject.SetActive(false);
        }

        LoadAttachment = ActiveWeapon.transform.GetChild(0);
        LoadAttachment = LoadAttachment.Find("Sight");
        for (int x = 0; x < LoadAttachment.childCount; x++)
        {
            LoadAttachment.GetChild(x).gameObject.SetActive(false);
        }


    }
    void LoadGunAssets()
    {

        Guns = new GameObject[GunAssets.childCount];
        for (int i = 0; i < GunAssets.childCount; i++)
        {
            Guns[i] = GunAssets.GetChild(i).gameObject;
        }
        
    }

    
    void Update()
    {
        ProcessInput();
    }

    private void AttachmentManager()
    {

        LoadUnderBarrelAttachments();
        LoadBarrelAttachments();
        LoadSightAttachments();
        SetActiveDisableAttachments();


    }
    void LoadBarrelAttachments()
    {
        LoadAttachment = ActiveWeapon.transform.GetChild(0);
        LoadAttachment = LoadAttachment.Find("Barrel");

        BarrelAttachments = new GameObject[LoadAttachment.childCount];

        for (int x = 0; x < LoadAttachment.childCount; x++)
        {
            BarrelAttachments[x] = LoadAttachment.transform.GetChild(x).gameObject;
        }
    }
    void LoadUnderBarrelAttachments()
    {
        LoadAttachment = ActiveWeapon.transform.GetChild(0);
        LoadAttachment = LoadAttachment.Find("UnderBarrel");

        UnderBarrelAttachments = new GameObject[LoadAttachment.childCount];

        for (int x = 0; x < LoadAttachment.childCount; x++)
        {
            UnderBarrelAttachments[x] = LoadAttachment.transform.GetChild(x).gameObject;
        }
    }
    void LoadSightAttachments()
    {
        LoadAttachment = ActiveWeapon.transform.GetChild(0);
        LoadAttachment = LoadAttachment.Find("Sight");

        SightAttachments = new GameObject[LoadAttachment.childCount];

        for (int x = 0; x < LoadAttachment.childCount; x++)
        {
            SightAttachments[x] = LoadAttachment.transform.GetChild(x).gameObject;
        }
    }

    private void ProcessInput()
    {
        ProcessMiddleMouseButton();
    }

    private void ProcessMiddleMouseButton()
    {
        if (Input.mouseScrollDelta.y > 0)
            ChangeWeapon(1);
        else if (Input.mouseScrollDelta.y < 0)
            ChangeWeapon(-1);
    }

    void ChangeWeapon(int x)
    {
        
        ActiveWeaponCursor += x;
        if (ActiveWeaponCursor < 0)
            ActiveWeaponCursor = Guns.Length-1;
        if (ActiveWeaponCursor == Guns.Length)
            ActiveWeaponCursor = 0;

        Destroy(ActiveWeapon);
        ActiveWeapon = Guns[ActiveWeaponCursor];
        ActiveWeapon = Instantiate(ActiveWeapon, GunSpawn);
        AttachmentManager();

    }
}
