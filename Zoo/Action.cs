using System;
using System.Linq;

namespace Zoo
{
    public interface IApplicationAction
    {
        public string Key { get; }

        public string Description { get; }

        public void Execute(IoProcessor ioProcessor, AnimalCollection animalCollection);
    }

    public class AnimalPrintAction : IApplicationAction
    {
        public string Key => "P";

        public string Description => "동물 정보 출력하기";

        public void Execute(IoProcessor ioProcessor, AnimalCollection animalCollection)
        {
            int animalCount = animalCollection.FindAllAnimals().Count();
            if (animalCount == 0)
            {
                ioProcessor.WriteLines("아직 등록된 동물이 없습니다.");
                return;
            }
            foreach (Animal animal in animalCollection.FindAllAnimals())
            {
                ioProcessor.WriteLines($"| 이름: {animal.Name}\t\t| 종: {animal.Species}\t\t |");
            }
        }
    }

    public class AnimalAddAction : IApplicationAction
    {
        public string Key => "A";

        public string Description => "동물 정보 등록하기";

        public void Execute(IoProcessor ioProcessor, AnimalCollection animalCollection)
        {
            string name = ioProcessor.ReadValue("동물의 이름을 입력해 주세요: ");
            while (string.IsNullOrWhiteSpace(name))
            {
                ioProcessor.WriteLines("정확한 동물의 이름을 입력해 주세요.");
                name = ioProcessor.ReadValue("동물의 이름을 입력해 주세요: ");
            }
            string species = ioProcessor.ReadValue("동물의 종을 입력해 주세요: ");
            while (string.IsNullOrWhiteSpace(species))
            {
                ioProcessor.WriteLines("정확한 동물의 종을 입력해 주세요.");
                species = ioProcessor.ReadValue("동물의 종을 입력해 주세요: ");
            }
            string id = Guid.NewGuid().ToString();
            Animal animal = new(id, name, species);
            if (animalCollection.AddAnimal(animal))
            {
                ioProcessor.WriteLines("동물 정보가 등록되었습니다.");
            }
            else
            {
                ioProcessor.WriteLines("중복된 동물 정보가 이미 등록돼 있습니다.");
            }
        }
    }
}
