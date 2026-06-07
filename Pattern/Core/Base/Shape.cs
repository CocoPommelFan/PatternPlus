using ADOFAI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatternPlus.Pattern.Core.Base
{
    public abstract class Shape
    {
        public float[] Angles { get; protected set; }

        private float[] _angles;
        private bool _isHalf;
        private bool _isPseudo;
        private float _pseudoAngle;
        private bool _isInversed;

        public List<scrFloor> Floors { get; private set; } = new();
        public scrFloor FirstFloor { get; private set; }
        public scrFloor LastFloor { get; private set; }

        public bool IsHalf => _isHalf;
        public virtual bool IsPseudo => _isPseudo;
        public bool isInversed => _isInversed;

        protected int MaxAngle => IsHalf ? 180 : 360;


        public Shape(bool isHalf, float pseudoAngle, bool isInversed)
        {
            _isHalf = isHalf;
            _pseudoAngle = pseudoAngle;
            _isInversed = isInversed;
        }
        public void Build()
        {
            var editor = Patches.EditorInstance.Instance;

            if (isInversed)
            {
                for (int i = 0; i < Angles.Length; i++)
                {
                    Angles[i] *= -1;
                }
            }

            foreach (float angle in Angles)
            {
                editor.CreateFloorWithCharOrAngle(angle, 'a');
            }

            LastFloor = editor.selectedFloors.FirstOrDefault();
            // +1 ПОТОМУ ЧТО В СПИСКЕ УГЛОВ ПЕРВАЯ ПЛИТКА ИМЕЕТ УГОЛ -999 И ОНА УЧИТЫВАЕТСЯ
            int targetSeqId = LastFloor.seqID - Angles.Length + 1;

            FirstFloor = editor.floors.FirstOrDefault(f => f.seqID == targetSeqId);

            Floors = editor.floors
                .Where(f => f.seqID >= FirstFloor.seqID && f.seqID <= LastFloor.seqID)
                .ToList();

            // ДОП ПЛИТКА ЧТОБЫ НЕ ОБОСРАТЬСЯ
            editor.CreateFloorWithCharOrAngle(Angles.Last(), 'a');
            FloorsUtils.AddSetSpeedToPatternStartAndEnd();
            FloorsUtils.AddRadiusScaleToPattern();

        }
        public abstract void Refresh();
    }
}
