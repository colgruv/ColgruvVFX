using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemAffector : MonoBehaviour
{
    protected ParticleSystem m_System;
    protected ParticleSystem.Particle[] m_Particles;

    protected void checkSystemInitialization()
    {
        if (m_System == null)
        {
            m_System = GetComponent<ParticleSystem>();
        }

        if (m_Particles == null || m_Particles.Length != m_System.main.maxParticles)
        {
            m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];
        }
    }
}
