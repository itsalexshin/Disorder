﻿using System.Collections;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class cut_fruit : MonoBehaviour {

    public GameObject cut_right;
    public GameObject cut_left;
    public GameObject breadslice;
    public GameObject knife;
    public Texture breadcrust_glow;
    public Texture carrot_glow;
    public Texture cucumber_glow;

    private int idx;
    private int racount;
    private int gacount;
    private int bcount;
    private Vector3 initial_knife_pos;
    private Quaternion initial_knife_rot;

    private float breadHeight;
    private float breadTopY;
    private float breadBotY;

    private bool entered;

    //Timer
    private float startTime;
    private float stopTime;
    private float totalTime;
    private bool started = false;
    private bool finished = false;
    public int counter = 0;
    private int breadcounter = 0;
    public StreamWriter file_left;
    public StreamWriter file_right;
    private String filename_left;
    private String filename_right;

    Material glowmaterial;


    // Use this for initialization
    void Start () {
        idx = 12;
        racount = 1;
        gacount = 1;
        bcount = 1;

        entered = false;

        filename_left = "LEFT_time_" + System.DateTime.Now.ToString("MM-dd-yy_hh-mm-ss") + ".txt";
        filename_right = "RIGHT_time_" + System.DateTime.Now.ToString("MM-dd-yy_hh-mm-ss") + ".txt";
        file_left = new StreamWriter(filename_left);
        file_right = new StreamWriter(filename_right);

        breadHeight = breadslice.GetComponent<Renderer>().bounds.size.y;
        breadTopY = breadslice.transform.position.y + (breadHeight / 2);
        breadBotY = breadslice.transform.position.y - (breadHeight / 2);

        initial_knife_pos = knife.transform.position;
        initial_knife_rot = knife.transform.rotation;

        glowmaterial = new Material(Shader.Find("Particles/Additive (Soft)"));
  }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        // BREAD
        if (other.CompareTag("Bread"))
        {
            if (counter == 0)
            {
                startTime = Time.time;
                counter++;
            }
            if (counter == 51)
            {
                startTime = Time.time;
                counter++;
            }
            if (other.gameObject.transform.parent.childCount > 1)
            {
                StartCoroutine(add_bread_tag(other.gameObject.transform.parent.GetChild(1)));
            }


            if (other.gameObject.transform.parent.childCount == 2 && bcount < idx)
            {
                StartCoroutine(delete_slice(other.gameObject.transform.parent.GetChild(1).gameObject));
                StartCoroutine(add_food(other.gameObject.transform.parent.parent.GetChild(bcount)));
                bcount++;
            }

            other.gameObject.transform.parent = null;
            other.gameObject.AddComponent<Rigidbody>();

            StartCoroutine(delete_slice(other.gameObject));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // RED APPLE
        /*if (other.collider.CompareTag("RedApple")) 
        {
            Debug.Log("apple child count = " + other.collider.gameObject.transform.parent.childCount);

            if (other.collider.gameObject.transform.parent.childCount == 1 && racount < idx)
            {
                StartCoroutine(add_food(other.collider.gameObject.transform.parent.parent.GetChild(racount)));
                racount++;
            }

            other.collider.gameObject.transform.parent = null;
            other.collider.gameObject.AddComponent<Rigidbody>();

            StartCoroutine(delete_slice(other.collider.gameObject));
        }

        // GREEN APPLE
        if (other.collider.CompareTag("GreenApple"))
        {

            if (other.collider.gameObject.transform.parent.childCount == 1 && gacount < idx)
            {
                StartCoroutine(add_food(other.collider.gameObject.transform.parent.parent.GetChild(gacount)));
                gacount++;
            }

            other.collider.gameObject.transform.parent = null;
            other.collider.gameObject.AddComponent<Rigidbody>();

            StartCoroutine(delete_slice(other.collider.gameObject));
        }*/

        // BREAD
        /*if (other.collider.CompareTag("Bread"))
        {
            if(counter == 0)
            {
                startTime = Time.time;
                counter++;
            }
            if (other.collider.gameObject.transform.parent.childCount > 1)
            {
                Debug.Log("hi");
                StartCoroutine(add_bread_tag(other.collider.gameObject.transform.parent.GetChild(1)));
            }


            if (other.collider.gameObject.transform.parent.childCount == 1 && bcount < idx)
            {
                StartCoroutine(add_food(other.collider.gameObject.transform.parent.parent.GetChild(bcount)));
                bcount++;
            }

            other.collider.gameObject.transform.parent = null;
            other.collider.gameObject.AddComponent<Rigidbody>();

            StartCoroutine(delete_slice(other.collider.gameObject));
        }*/

        /*if (other.collider.CompareTag("Bread"))
        {
            // The knife is around the top of the bread slice.
            if (this.transform.position.y < (breadTopY + (breadTopY * .1)) && this.transform.position.y > (breadTopY - (breadTopY * .1)))
            {
                entered = true;
            }

        }*/
    }

    /*private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Bread"))
        {
            // The knife has passed the top of the bread slice and can now actually cut the bread if the knife has passed the top
            if (entered == true && this.transform.position.y < (breadBotY + (breadBotY * .1)) && this.transform.position.y > (breadBotY - (breadBotY * .1)))
            {
                if (counter == 0)
                {
                    startTime = Time.time;
                    counter++;
                }
                if (counter == 51)
                {
                    startTime = Time.time;
                    counter++;
                }
                if (collision.gameObject.transform.parent.childCount > 1)
                {
                    StartCoroutine(add_bread_tag(collision.gameObject.transform.parent.GetChild(1)));
                }


                if (collision.gameObject.transform.parent.childCount == 2 && bcount < idx)
                {
                    StartCoroutine(delete_slice(collision.gameObject.transform.parent.GetChild(1).gameObject));
                    StartCoroutine(add_food(collision.gameObject.transform.parent.parent.GetChild(bcount)));
                    bcount++;
                }

                collision.gameObject.transform.parent = null;
                collision.gameObject.AddComponent<Rigidbody>();

                StartCoroutine(delete_slice(collision.gameObject));
                entered = false;
            }
        }
    }*/

    IEnumerator add_bread_tag(Transform go)
    {
        yield return new WaitForSeconds(.5f);
        //yield return new WaitForSeconds(0.1f);
        go.tag = "Bread";
        go.GetComponent<Renderer>().material.shader = Shader.Find("FX/Flare");
        go.GetComponent<BoxCollider>().enabled = true;
    }

    IEnumerator add_cucumber_tag(Transform go)
    {
      yield return new WaitForSeconds(1f);
      //yield return new WaitForSeconds(0.1f);
      go.tag = "Cucumber";
      go.GetComponent<BoxCollider>().enabled = true;
    }

    IEnumerator add_carrot_tag(Transform go)
    {
      yield return new WaitForSeconds(1f);
      //yield return new WaitForSeconds(0.1f);
      go.tag = "Carrot";
      go.GetComponent<BoxCollider>().enabled = true;
    }

  IEnumerator delete_slice(GameObject other)
    {
        breadcounter++;
        Debug.Log(breadcounter);
        if (breadcounter == 50)
        {
            stopTime = Time.time;
            totalTime = stopTime - startTime;
            Debug.Log("totalTime = " + totalTime.ToString() + " stopTime = " + stopTime.ToString() + " startTime = " + startTime.ToString());
            file_right.WriteLine("Total Time in min:sec = " + Math.Floor(totalTime/60) + ":" + Math.Round(totalTime % 60));
            file_right.Close();

            startTime = Time.time;

            StartCoroutine(change_sign(cut_left, cut_right));

            //knife.transform.SetPositionAndRotation(initial_knife_pos, initial_knife_rot);
        }

        if (breadcounter == 99)
        {
            stopTime = Time.time;
            totalTime = stopTime - startTime;
            Debug.Log("totalTime = " + totalTime.ToString() + " stopTime = " + stopTime.ToString() + " startTime = " + startTime.ToString());
            file_left.WriteLine("Total Time in min:sec = " + Math.Floor(totalTime / 60) + ":" + Math.Round(totalTime % 60));
            file_left.Close();

            cut_left.SetActive(false);
            StartCoroutine(back_to_main_menu());
        }
        yield return new WaitForSeconds(2.5f);
        //yield return new WaitForSeconds(0.2f);
        other.SetActive(false);
    }

    IEnumerator back_to_main_menu()
    {
      yield return new WaitForSeconds(6f);
      SceneManager.LoadScene(0);
    }

    IEnumerator add_food(Transform A)
    {
        yield return new WaitForSeconds(4f);
        //yield return new WaitForSeconds(1f);
        A.gameObject.SetActive(true);
    }

    IEnumerator change_sign(GameObject sign_left, GameObject sign_right)
      {
        yield return new WaitForSeconds(3f);
        //yield return new WaitForSeconds(1f);
        sign_right.SetActive(false);
        sign_left.SetActive(true);
      }
}
