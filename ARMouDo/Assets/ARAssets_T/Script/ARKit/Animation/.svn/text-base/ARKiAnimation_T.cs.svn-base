using System.Collections;
using System.Collections.Generic;
using Tools_XYRF;
using UnityEngine;


namespace ARKit_T
{
    /// <summary>
    /// ARKit 动画
    /// </summary>
    public class ARKiAnimation_T : MonoBehaviour
    {
        /// <summary>
        /// 动画控制器
        /// </summary>
        private Animator animatorControl;
        /// <summary>
        /// 动画状态信息
        /// </summary>
        private AnimatorStateInfo animatorStateInfo;
        /// <summary>
        /// 闲置动画
        /// </summary>
        private AnimationClip idle = null;
        /// <summary>
        /// 移动动画
        /// </summary>
        private AnimationClip move = null;
        /// <summary>
        /// 动画 
        /// </summary>
        private List<AnimationClip> animationStock = new List<AnimationClip>();
        /// <summary>
        /// 不加入动画存库的 命名数
        /// </summary>
        private int dontAddAnimation = 30;
        /// <summary>
        /// 随机动画索引
        /// </summary>
        private int randomIndex = 0;

        private void Start()
        {
            ARKiAnimation_TInit();
        }

        private void OnDisable()
        {
            instance = null;
        }
        private void OnDestroy()
        {
            instance = null;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void ARKiAnimation_TInit()
        {
            if (transform.GetComponent<Animator>() != null)
            {
                animatorControl = transform.GetComponent<Animator>();
                if (animatorControl.runtimeAnimatorController != null)
                {
                    AnimationClip[] animatorClipInfos = animatorControl.runtimeAnimatorController.animationClips;
                    for (int i = 0; i < animatorClipInfos.Length; i++)
                    {
                        int dex = 0;
                        for (int c = 0; c < dontAddAnimation; c++)
                        {
                            if (animatorClipInfos[i].name != c.ToString())
                                dex += 1;
                        }
                        if (dex == dontAddAnimation)  //没有排除的动画名字
                        {
                            if (animatorClipInfos[i].name == "Idle" || animatorClipInfos[i].name == "idle")
                            {
                                idle = animatorClipInfos[i];
                            }
                            else if (animatorClipInfos[i].name == "Move" || animatorClipInfos[i].name == "move" ||
                                animatorClipInfos[i].name == "Walk" || animatorClipInfos[i].name == "walk")
                            {
                                move = animatorClipInfos[i];
                            }
                            else
                            {
                                animationStock.Add(animatorClipInfos[i]);
                            }
                        }
                    }
                }

                randomIndex = 0;
                instance = this;
            }
        }
        /// <summary>
        /// 判断动画 是否存在
        /// </summary>
        /// <param name="aRAnimaitonType">动画类型</param>
        /// <returns>true= 存在动画</returns>
        public bool JudeModeAnimation(ARAnimaitonType aRAnimaitonType)
        {
            switch (aRAnimaitonType)
            {
                case ARAnimaitonType.Idle:
                    if (idle != null)
                    {
                        return true;
                    }
                    break;
                case ARAnimaitonType.Move:
                    if (move != null)
                    {
                        return true;
                    }
                    break;
                case ARAnimaitonType.Random:
                    if (animationStock.Count > 0)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        /// <summary>
        /// 播放 模型动画
        /// </summary>
        /// <param name="aRAnimaitonType">动画类型</param>
        public void PlayModeAnimation(ARAnimaitonType aRAnimaitonType)
        {
            switch (aRAnimaitonType)
            {
                case ARAnimaitonType.Idle:
                    if (idle != null)
                    {
                        animatorControl.Play(idle.name);
                    }
                    break;
                case ARAnimaitonType.Move:
                    if (move != null)
                    {
                        animatorControl.Play(move.name);
                    }
                    break;
                case ARAnimaitonType.Random:
                    if (animationStock.Count > 0)
                    {
                        if (randomIndex == animationStock.Count)
                            randomIndex = 0;
                        animatorControl.Play(animationStock[randomIndex].name);
                        randomIndex++;
                        //   StartCoroutine(DetectionPlayProgress());
                    }
                    break;
            }
        }
        /// <summary>
        /// 检测 动画是否播放完成
        /// </summary>
        /// <returns></returns>
        private IEnumerator DetectionPlayProgress()
        {
            bool isPlayOver = false;
            while (!isPlayOver)
            {
                yield return new WaitForSeconds(0.02f);
                animatorStateInfo = animatorControl.GetCurrentAnimatorStateInfo(0);
                if (animatorStateInfo.normalizedTime >= 1f)
                {
                    PlayModeAnimation(ARAnimaitonType.Idle);
                    isPlayOver = true;
                }
            }
        }



        private static ARKiAnimation_T instance = null;
        public static ARKiAnimation_T Instance
        {
            get
            {
                return instance;
            }
        }

    }
    /// <summary>
    /// AR 模型动画类型
    /// </summary>
    public enum ARAnimaitonType
    {
        /// <summary>
        /// 闲置
        /// </summary>
        Idle,
        /// <summary>
        /// 移动
        /// </summary>
        Move,
        /// <summary>
        /// 随机
        /// </summary>
        Random
    }
}
