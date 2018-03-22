package com.example.cwt59.fastbreak;

import android.app.Service;
import android.content.Intent;
import android.os.CountDownTimer;
import android.os.IBinder;
import android.util.Log;
import android.os.Binder;
import android.os.Bundle;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;
import java.util.concurrent.TimeUnit;

import android.util.Log;

import android.view.View;
import android.widget.Toast;

public class MyService extends Service {
    private static final String TAG = "com.cwt59.myTest";
    private final IBinder caseysBinder = new MyLocalBinder();

    // hours updated by UI in Begin Fast
    private static int hours;

    public static long hour;

    // remainHours to keep track of remaining hours until you can eat something legit
    private static double remainHours;
    // ounces to keep track of water drinking gauge
    private static int ounces;

    //broadcast tools below for CountDownTimer
    private final static String TAJ = "BroadcastService";
    public static final String COUNTDOWN_BR ="com.example.cwt59.fastbreak";
    Intent bi = new Intent(COUNTDOWN_BR);



    CountDownTimer cdt = null;
    @Override
    public void onCreate(){
        super.onCreate();

        Log.i(TAJ, "starting timer...");
        long myHour = hour;
        //I need to be able to pass in hours without changing the integrity of time.
        long time = 24 * 3600000;
        cdt = new CountDownTimer(time, 3330){
            @Override
            public void onTick(long millisUntilFinished){
                long hours = TimeUnit.MILLISECONDS.toHours(millisUntilFinished);

                Log.i(TAJ,"Countdown seconds remaining: " + millisUntilFinished/1000);
                remainHours = millisUntilFinished/36000000;
                bi.putExtra("countdown",millisUntilFinished);
                sendBroadcast(bi);
            }

            @Override
            public void onFinish(){
                Log.i(TAJ,"Timer finished");

                onDestroy();
            }
        };
        cdt.start();


    }

    @Override
    public void onDestroy(){

        Log.i(TAJ, "Activity destroyed");

    }

    public MyService(){

    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId){
        String iString = intent.getStringExtra("Hours");

        //get the amount of hours to fast
        hours = Integer.valueOf(iString);
        System.out.println(hours);
        //get committed hours in long format for broadcast
        hour =  (long) hours;
        //get the amount of water to drink
        ounces = (int)(((double) hours / 12) * 16) ;


        Toast.makeText(this,iString,Toast.LENGTH_LONG).show();
        Log.i(TAG, "onStart received the hours!");

        return super.onStartCommand(intent, flags, startId);
    }


    @Override
    public IBinder onBind(Intent intent){
        Log.i(TAG, "The binder was returned!");
        return caseysBinder;
    }

   // public String getCurrentTime(){
        //SimpleDateFormat df = new SimpleDateFormat("HH:mm:ss, Local.US");
        //Log.i(TAG, "The date was reached...changed?");
        //return(df.format(new Date()));

        //doesn't work.
     //   return null;
   // }
    public String getWater(){

        String water = "water water ";
        String mWater =  new String(new char[ounces]).replace("\0", water);

        return mWater;
    }

    // get hour
    public long getHour(){
        return hour;
    }

    //get hours
    public int getHours(){
        return hours;
    }
    public int getOunces(){
        return ounces;
    }

    public void drinkWater(){
        ounces = ounces -1;
    }

    public String getString(){
        String myString = "I come from the Service on another thread running in the background. hello World";
        return(myString);

    }

    public class MyLocalBinder extends Binder {
        MyService getService(){
            //Log.i(TAG, "The binder was reached");
            return MyService.this;

        }
    }
}
