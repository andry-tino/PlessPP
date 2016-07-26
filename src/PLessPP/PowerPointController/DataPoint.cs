using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPointController
{
    public class DataPoint
    {
        private double AcceleartionX { get; }
        private double AcceleartionY { get; }
        private double AcceleartionZ { get; }
        private double AngularVelocityX { get; }
        private double AngularVelocityY { get; }
        private double AngularVelocityZ { get; }
        public long Ticks { get; }

        public DataPoint(double acceleartionX,
            double acceleartionY,
            double acceleartionZ,
            double angularVelocityX,
            double angularVelocityY,
            double angularVelocityZ,
            long ticks)
        {
            this.AcceleartionX = acceleartionX;
            this.AcceleartionY = acceleartionY;
            this.AcceleartionZ = acceleartionZ;
            this.AngularVelocityX = angularVelocityX;
            this.AngularVelocityY = angularVelocityY;
            this.AngularVelocityZ = angularVelocityZ;
            this.Ticks = ticks;
        }

        public override String ToString()
        {
            return
                $"{this.AcceleartionX},{this.AcceleartionY},{this.AcceleartionZ},{this.AngularVelocityX},{this.AngularVelocityY},{this.AngularVelocityZ},{this.Ticks}";
        }
    }
}
