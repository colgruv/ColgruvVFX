using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeSpell : MonoBehaviour
{
    public ParticleSystem SpellParticles;
    public PSGravitonEffect SpellGravity;
    public AudioSource SpellSound;

    public Vector2 SoundPitchWindow;
    public Vector2 SoundVolumeWindow;
    public Vector2 ParticleAlphaWindow;
    public Vector2 ParticleGravityWindow;
    public Vector2 ParticleNoiseWindow;

    // 1 / amount of time it takes to fully charge spell while holding down
    public float ChargeSpeed = 0.25f;

    // 1 / amound of time it takes for spell to dissipate when not holding down
    public float DischargeSpeed = 1f;

    private float m_CurrentCharge;

	// Use this for initialization
	void Start () {
        m_CurrentCharge = 0f;

        if (SpellParticles == null)
            SpellParticles = GetComponent<ParticleSystem>();

        if (SpellSound == null)
            SpellSound = GetComponent<AudioSource>();

        if (SpellGravity == null)
            SpellGravity = GetComponent<PSGravitonEffect>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetMouseButton(0))
        {
            m_CurrentCharge += ChargeSpeed * Time.deltaTime;
        }	
        else
        {
            m_CurrentCharge -= DischargeSpeed * Time.deltaTime;
        }

        m_CurrentCharge = Mathf.Min(1f, m_CurrentCharge);
        m_CurrentCharge = Mathf.Max(0f, m_CurrentCharge);

        applyChargeToAttributes();
	}

    private void applyChargeToAttributes()
    {
        // Particle enabled
        SpellParticles.gameObject.SetActive(m_CurrentCharge > 0f);

        // Particle gravity
        SpellGravity.GravityModifier = Mathf.Lerp(ParticleGravityWindow.x, ParticleGravityWindow.y, m_CurrentCharge);

        // Particle alpha
        Color color = SpellParticles.GetComponent<Renderer>().material.GetColor("_TintColor");
        color.a = Mathf.Lerp(ParticleAlphaWindow.x, ParticleAlphaWindow.y, m_CurrentCharge);
        SpellParticles.GetComponent<Renderer>().material.SetColor("_TintColor", color);

        // Particle noise
        ParticleSystem.NoiseModule noise = SpellParticles.noise;
        noise.strength = Mathf.Lerp(ParticleNoiseWindow.x, ParticleNoiseWindow.y, m_CurrentCharge);

        // Sound pitch
        SpellSound.pitch = Mathf.Lerp(SoundPitchWindow.x, SoundPitchWindow.y, m_CurrentCharge);

        // Sound volume
        SpellSound.volume = Mathf.Lerp(SoundVolumeWindow.x, SoundVolumeWindow.y, m_CurrentCharge);
    }
}
