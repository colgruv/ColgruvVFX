using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSGravitonEffect : ParticleSystemAffector
{
    public float GravityModifier = 10f;
	
	// Update is called once per frame
	void LateUpdate ()
    {
        checkSystemInitialization();

        // Retrieve particles from system
        int numParticlesAlive = m_System.GetParticles(m_Particles);

        // Retrieve the radius of the emission shell to determine maximum distance
        float radius = m_System.shape.radius * 1.05f;

        // Only modify particles that are currently alive
        for (int i = 0; i < numParticlesAlive; i++)
        {
            // Particles move toward the origin
            Vector3 newVelocity = m_Particles[i].position * -1f;

            // Particles move slower the farther they are from the origin
            float velocityMultiplier = 1f - (m_Particles[i].position.magnitude / radius);
            newVelocity *= velocityMultiplier;

            // Particle speed is multiplied by Gravity Modifier
            newVelocity *= GravityModifier;

            // Apply new velocity
            m_Particles[i].velocity = newVelocity;
        }

        // Store particles in system
        m_System.SetParticles(m_Particles, numParticlesAlive);
	}
}
