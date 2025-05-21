using System;

namespace Zoo
{
    /*
    역할 : 동물의 종 표현
    책임 : Species 이름 유효성 검사, Species Equals 비교, 
    Species - String 암시적 변환(짜피 state가 string _value 밖에 없으니까), Species 간 연산자 오버라이딩 
    */
    public class Species
    {
        private readonly string _value;

        public Species(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "Cannot accept an empty or white space string for a species name.", nameof(value));
            }
            _value = value;
        }

        public string Value => _value;

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj is not Species other)
            {
                return false;
            }
            return Value.Equals(other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(Species species) => species.Value;

        public static implicit operator Species(string species) => new(species);

        public static bool operator ==(Species x, Species y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(Species x, Species y)
        {
            return !x.Equals(y);
        }
    }
}
