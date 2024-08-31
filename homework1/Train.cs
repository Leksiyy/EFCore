namespace homework1;

// train как вагон
// id
// color
// IsCoupe
// volume
// maxSpeed
// year


public class Train
{
    public int? Id { get; set; }
    public int Color { get; set; }
    public bool IsCoupe { get; set; }
    public short Volume { get; set; }
    public short MaxSpeed { get; set; }
    public DateTime Year { get; set; }
}