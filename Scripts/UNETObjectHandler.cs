using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;


public class UNETObjectHandler : NetworkBehaviour
{


    private UNETSyncObjects LocalGameSystem;

    // Use this for initialization
    void Start () {
        if (GameObject.Find("LocalGameSystem")!=null) {
            LocalGameSystem = GameObject.Find("LocalGameSystem").GetComponent<UNETSyncObjects>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (LocalGameSystem != null)
        {
            if (LocalGameSystem.ObjectsToModify.Count > 0 && isLocalPlayer)
            {
                ModifyObjects();
            }
        }
	}


    // ********************* Roll object
    [Command]// (serveur)
    public void CmdRollObject(GameObject obj, NetworkInstanceId nii)
    {
        RpcRollObject(obj, nii);
        CmdModifyInterract(obj, !(nii == NetworkInstanceId.Invalid));
    }
    [ClientRpc]// (clients)
    private void RpcRollObject(GameObject obj, NetworkInstanceId nii)
    {
        obj.GetComponent<RollAble>().Roll(nii);
    }

    
    // ********************* Slice object
    [Command]// (serveur)
    public void CmdSliceObject(GameObject obj, NetworkInstanceId nii)
    {
        RpcSliceObject(obj, nii);
        CmdModifyInterract(obj, !(nii == NetworkInstanceId.Invalid));
    }
    [ClientRpc]// (clients)
    private void RpcSliceObject(GameObject obj, NetworkInstanceId nii)
    {
        obj.GetComponent<Slice>().slice(nii);
    }

    // ********************* Move object
    [Command]// (serveur)
    public void CmdMoveObject(GameObject obj, NetworkInstanceId nii)
    {
        RpcMoveObject(obj, nii);
        CmdModifyInterract(obj, !(nii == NetworkInstanceId.Invalid));
    }
    [ClientRpc]// (clients)
    private void RpcMoveObject(GameObject obj, NetworkInstanceId nii)
    {
        obj.GetComponent<MouseDrag>().DragSourie(nii);
    }


    // *********************  Change interraction
    [Command]// (serveur)
    public void CmdModifyInterract(GameObject obj, bool i)
    {
        RpcchangeInterract(obj, i);
    }

    [ClientRpc]// (clients)
    public void RpcchangeInterract(GameObject obj, bool i)
    {
        obj.GetComponent<Interractable>().interract = i;
    }


    // ***************  Set object on fire
    [Command]// (serveur)
    public void CmdSetFire(GameObject obj, bool isOnFire)
    {
        RpcactivateFire(obj, isOnFire);
    }
    [ClientRpc]// (clients)
    public void RpcactivateFire(GameObject obj, bool isOnFire)
    {
        obj.GetComponent<InflamAble>().OnFire = isOnFire;
    }
    
    
    // ***************  Destroy object
    [Command]// (serveur)
    public void CmdDestroy(GameObject obj)
    {
        RpcDestroy(obj);
    }
    [ClientRpc]// (clients)
    public void RpcDestroy(GameObject obj)
    {
        Destroy(obj);
    }


    // ***************  Active object
    [Command]// (serveur)
    public void CmdActiveObject(GameObject obj, bool active)
    {
        RpcActiveObject(obj, active);
    }
    [ClientRpc]// (clients)
    public void RpcActiveObject(GameObject obj, bool active)
    {
        obj.SetActive(active);
    }

    [Command]// (serveur)
    public void CmdAddLocalAuthority(GameObject obj)
    {
        NetworkInstanceId nIns = obj.GetComponent<NetworkIdentity>().netId;
        GameObject client = NetworkServer.FindLocalObject(nIns);
        NetworkIdentity ni = client.GetComponent<NetworkIdentity>();
        ni.AssignClientAuthority(connectionToClient);
    }

    [Command]// (serveur)
    public void CmdRemoveLocalPlayerAuthority(GameObject obj)
    {
        NetworkInstanceId nIns = obj.GetComponent<NetworkIdentity>().netId;
        GameObject client = NetworkServer.FindLocalObject(nIns);
        NetworkIdentity ni = client.GetComponent<NetworkIdentity>();
        ni.RemoveClientAuthority(ni.clientAuthorityOwner);
    }

    private void ModifyObjects()
    {
        foreach (Modifications modif in LocalGameSystem.ObjectsToModify.ToArray())
        {
            CmdAddLocalAuthority(modif.ObjectToModify);
            switch (modif.ToDo)
            {
                case (ToDo.setFire):
                    CmdSetFire(modif.ObjectToModify, bool.Parse(modif.Value));
                    break;
                case (ToDo.interract):
                    CmdModifyInterract(modif.ObjectToModify, bool.Parse(modif.Value));
                    break;
                case (ToDo.move):
                    CmdMoveObject(modif.ObjectToModify, modif.Hand);
                    break;
                case (ToDo.slice):
                    CmdSliceObject(modif.ObjectToModify, modif.Hand);
                    break;
                case (ToDo.destroy):
                    CmdDestroy(modif.ObjectToModify);
                    break;
                case (ToDo.roll):
                    CmdRollObject(modif.ObjectToModify, modif.Hand);
                    break;
                case (ToDo.activate):
                    CmdActiveObject(modif.ObjectToModify, bool.Parse(modif.Value));
                    break;
            }
            LocalGameSystem.ObjectsToModify.Remove(modif);
            CmdRemoveLocalPlayerAuthority(modif.ObjectToModify);
        }
    }
}
