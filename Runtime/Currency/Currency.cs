using System;
using System.Numerics;

namespace LumosLib
{
    public class Currency
    {
        public int ID { get; }
        public BigInteger Value { get; private set; }

        public event Action<BigInteger, BigInteger> OnChanged;

        
        public Currency(int id)
        {
            ID = id;
        }

        
        public Currency(int id, BigInteger initialValue) : this(id)
        {
            if (initialValue > 0)
            {
                Value = initialValue;
            }
        }

        
        public void Set(BigInteger newValue)
        {
            BigInteger previous = Value;
            Value = newValue < BigInteger.Zero ? BigInteger.Zero : newValue;
            
            if (Value < 0)
            {
                Value = 0;
            }

            if (previous != Value)
            {
                OnChanged?.Invoke(previous, Value);
            }
        }

        
        public void Add(BigInteger amount)
        {

            Set(Value + amount);
        }
        
        
        public bool Consume(BigInteger amount)
        {
            if (amount <= BigInteger.Zero || Value < amount) 
                return false;
            
            Set(Value - amount);
            
            return true;
        }
    }
}
