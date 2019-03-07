
Sous réserve de déclarer les variables.
L'étape initiale est l'étape Xs0.

````
private void runCylceApiStores()
{
    Debug.WriteLine("*********** Cycle ***********");

    this.Xs0prec = this.Xs0; this.Xs1prec = this.Xs1; this.Xs2prec = this.Xs2;
    this.upPrec = this.up; this.downPrec = this.down;

    this.up = MemoryMap.Instance.GetBit(3, MemoryType.Input).Value;
    this.down = MemoryMap.Instance.GetBit(4, MemoryType.Input).Value;
    this.volet = MemoryMap.Instance.GetFloat(3, MemoryType.Input).Value;

    if (this.volet == 0)
    {
        this.haut = false;this.bas = true;
    } else if (this.volet == 10.0)
    {
        this.haut = true;this.bas = false;
    }
    else
    {
        this.haut = false;this.bas = false;
    }

    this.frontUp = !this.upPrec && this.up;
    this.frontDown = !this.downPrec && this.down;

    bool ft1s = this.Xs0 && this.frontUp;
    bool ft2s = this.Xs1 && this.haut;
    bool ft3s = this.Xs1 && this.frontDown;
    bool ft4s = this.Xs0 && this.frontDown;
    bool ft5s = this.Xs2 && this.bas;
    bool ft6s = this.Xs2 && this.frontUp;

    this.Xs0 = (ft2s || ft5s) || this.Xs0prec && !(ft1s || ft4s);
    this.Xs1 = (ft1s || ft6s) || this.Xs1prec && !(ft2s || ft3s);
    this.Xs2 = (ft3s || ft4s) || this.Xs2prec && !(ft5s || ft6s);

    MemoryBit Monter = MemoryMap.Instance.GetBit(1, MemoryType.Output);
    MemoryBit Descendre = MemoryMap.Instance.GetBit(2, MemoryType.Output);
    Monter.Value = this.Xs1;
    Descendre.Value = this.Xs2;

    MemoryMap.Instance.Update();
}
````
