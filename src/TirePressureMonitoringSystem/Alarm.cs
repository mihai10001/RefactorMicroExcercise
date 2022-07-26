namespace TDDMicroExercises.TirePressureMonitoringSystem
{
    public class Alarm
    {
        private const double LowPressureThreshold = 17;
        private const double HighPressureThreshold = 21;

        private readonly Sensor _sensor = new Sensor();
        private bool _alarmOn = false;

        public bool AlarmOn
        {
            get { return _alarmOn; }
        }

        public Alarm() {}

        public Alarm(Sensor sensor)
        {
            _sensor = sensor;
        }

        public void Check()
        {
            double psiPressureValue = _sensor.PopNextPressurePsiValue();
            if (!isPsiPressureValid(psiPressureValue))
            {
                _alarmOn = true;
            }
        }

        private bool isPsiPressureValid(double psiPressureValue)
        {
            return psiPressureValue > LowPressureThreshold && psiPressureValue < HighPressureThreshold;
        }
    }
}
