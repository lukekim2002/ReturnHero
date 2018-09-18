using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CNavigationData : MonoBehaviour
{
    //맵의 폭과 높이
    int m_iWidth = 0;
    int m_iHeight = 0;

    byte[] m_MapData = null;    //단순히 0과 1로 표현되는 맵데이터

    //해당 지점이 이동가능한 지점인가 확인한다
    virtual public bool IsValidPos(int x, int y)
    {
        if (x < 0 || x >= m_iWidth) return false;
        if (y < 0 || y >= m_iHeight) return false;

        if (m_MapData[(y * m_iWidth) + x] == 0) return false;
        return true;
    }

    //해당 지점이 이동가능한 지점인가 확인한다
    virtual public bool IsValidPos(CNaviNode pos)
    {
        if (pos.x < 0 || pos.x >= m_iWidth) return false;
        if (pos.y < 0 || pos.y >= m_iHeight) return false;

        if (m_MapData[(pos.y * m_iWidth) + pos.x] == 0) return false;
        return true;
    }


    //해당 노드에 인접한 이동가능한 이웃노드들을 모두구한다, 리턴값은 이웃의 개수
    virtual public int GetNeighbor(CNaviNode pos, ref List<CNaviNode> vecList)
    {
        int[] distx = new int[3] { -1, 0, 1 };
        int[] disty = new int[3] { -1, 0, 1 };

        for (int y = 0; y < 3; ++y)
        {
            for (int x = 0; x < 3; ++x)
            {
                int cx = distx[x] + pos.x;
                int cy = disty[y] + pos.y;
                if (cx == pos.x && cy == pos.y) continue;

                if (!IsValidPos(cx, cy)) continue;
                vecList.Add(CNaviNode.Create(cx, cy));
            }
        }

        return vecList.Count;
    }

    //맵의 폭과 높이
    public int GetWidth() { return m_iWidth; }
    public int GetHeight() { return m_iHeight; }
}
