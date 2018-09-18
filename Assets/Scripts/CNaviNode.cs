using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNaviNode : MonoBehaviour
{
    public int x, y; // 위치
    public int dist; // 목표점까지의 거리
    public int depth;

    public CNaviNode parent = null;

    // 현재 노드를 주어진 노드의 내용물로 복사
    public void Copy(CNaviNode pNode)
    {
        x = pNode.x;
        y = pNode.y;
        dist = pNode.dist;
        depth = pNode.depth;
        parent = pNode.parent;
    }

    // 위치가 같은지 확인
    public bool IsSamePos(CNaviNode pNode)
    {
        if (x != pNode.x || y != pNode.y) return false;

        return true;
    }

    // 내용물을 그대로 복사
    public CNaviNode Clone()
    {
        CNaviNode pNode = new CNaviNode();

        pNode.x = x;
        pNode.y = y;
        pNode.dist = dist;
        pNode.depth = depth;
        pNode.parent = null;

        return pNode;
    }

    //노드의 위치를 sx, sy로 설정하고, 목표점 dx, dy까지의 거리를 구해서 초기화 한다. , dep는 탐색깊이
    public static CNaviNode Create(int sx, int sy, int dx, int dy, int dep)
    {
        CNaviNode pNode = new CNaviNode();
        pNode.x = sx;
        pNode.y = sy;

        int deltx = dx - sx;
        int delty = dy - sy;

        pNode.dist = (deltx * deltx) + (delty * delty);
        pNode.depth = dep;

        return pNode;
    }

    //단순히 시작점 sx, sy로 해서 노드를 생성한다
    public static CNaviNode Create(int sx, int sy)
    {
        CNaviNode pNode = new CNaviNode();

        pNode.x = sx;
        pNode.y = sy;

        return pNode;
    }

    //주어진 목표점 까지의 거리를 구하고 탐색깊이를 설정한다.
    public void CalcDist(CNaviNode pDest, int cdepth)
    {
        int deltx = pDest.x - x;
        int delty = pDest.y - y;

        dist = (deltx * deltx) + (delty * delty);
        depth = cdepth;
    }

    public void SetParent(CNaviNode p) { parent = p; }

    public CNaviNode GetParent() { return parent; }
}