using UnityEngine;

public static class InputManager
{

    private static Controls _ctrls;

    private static Vector3 _mousePos;

    private static Camera cam;
    public static Ray GetCameraRay()
    {
        return cam.ScreenPointToRay(_mousePos);
    }




    public static void Init(Player p)
    {
        _ctrls = new();
        cam = Camera.main;
        
        _ctrls.Permenanet.Enable();

        _ctrls.InGame.Shoot.performed += _ =>
        {
            p.Shoot();
        };
        _ctrls.Permenanet.MousePos.performed += ctx =>
        {
            _mousePos = ctx.ReadValue<Vector2>();
        };

        _ctrls.InGame.Move.performed += ctx =>
        {
            p.SetMoveDirection(ctx.ReadValue<Vector3>());
        };


        _ctrls.InGame.SwitchWeapon1.performed += ctx => p.SwitchWeapon1();
        _ctrls.InGame.SwitchWeapon2.performed += ctx => p.SwitchWeapon2();
        _ctrls.InGame.SwitchWeapon3.performed += ctx => p.SwitchWeapon3();

        

        _ctrls.InGame.Reload.performed += ctx => p.Reload();
    }

    public static void EnableInGame()
    {
        _ctrls.InGame.Enable();
    }
}
