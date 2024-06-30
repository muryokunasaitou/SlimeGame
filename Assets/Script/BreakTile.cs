using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakTile : MonoBehaviour
{
    //�Ȃ񂩂�����Particle������ăG�f�B�^�ŃA�^�b�`���܂��傤
    [SerializeField] private ParticleSystem particle;


    private void Update()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D ot)
    {
        if (ot.gameObject.name == "Tilemap_B_Break_Ground")
        {
            var hitPos = Vector3.zero;
            //���������ꏊ�̍��W���擾
            foreach (var point in ot.contacts)
            {
                hitPos = point.point;
            }

            var position = ot.gameObject.GetComponent<Tilemap>().cellBounds.allPositionsWithin;
            //��ԋ߂��ꏊ��ۑ��������̂ŕϐ���錾
            var minPosition = 0;
            var allPosition = new List<Vector3>();

            foreach (var variable in position)
            {
                if (ot.gameObject.GetComponent<Tilemap>().GetTile(variable) != null)
                {
                    allPosition.Add(variable);
                }
            }

            //for���ŒT������B�ł���������0����Ă邩��1����X�^�[�g
            for (var i = 1; i < allPosition.Count; i++)
            {
                //���ꂼ��̂��������ꏊ����̑傫�����擾�A�ŏ����X�V������minPositionNum���X�V����
                if ((hitPos - allPosition[i]).magnitude <
                    (hitPos - allPosition[minPosition]).magnitude)
                {
                    minPosition = i;
                }
            }

            //�ŏI�I�Ȉʒu����U�i�[�����BRoundToInt�͎l�̌ܓ��Ƃ̂��Ƃł�
            var finalPosition = Vector3Int.RoundToInt(allPosition[minPosition]);

            var tiletmp = ot.gameObject.GetComponent<Tilemap>().GetTile(finalPosition);

            if (tiletmp != null)
            {
                var map = ot.gameObject.GetComponent<Tilemap>();
                var tileCol = ot.gameObject.GetComponent<TilemapCollider2D>();
                map.SetTile(finalPosition, null);
                tileCol.enabled = false;
                tileCol.enabled = true;

                //�킽����1�b�����ŃT�C�Y��ς��āA0.85f�ŏ������炢�������ɂȂ����̂ł���Ȋ����ɂ��܂���
                Destroy(Instantiate(particle, finalPosition + Vector3.one * 0.5f, Quaternion.identity), 0.85f);
            }
        }
    }
}
