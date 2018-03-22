package com.example.cwt59.fastbreak;

import android.app.Service;
import android.content.Intent;
import android.os.IBinder;
import android.util.Log;
import android.os.Binder;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;
import android.util.Log;
import android.widget.Toast;

public class MyService extends Service {
    private static final String TAG = "com.cwt59.myTest";
    private final IBinder caseysBinder = new MyLocalBinder();
    private static String hours;
    public MyService(){

    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId){
        hours = intent.getStringExtra("Hours");
        System.out.println(hours);
        Toast.makeText(this,hours,Toast.LENGTH_LONG).show();
        Log.i(TAG, "onStart received the hours!");
        return START_STICKY;
    }


    @Override
    public IBinder onBind(Intent intent){
        Log.i(TAG, "The binder was returned!");
        return caseysBinder;
    }

    public String getCurrentTime(){
        //SimpleDateFormat df = new SimpleDateFormat("HH:mm:ss, Local.US");
        //Log.i(TAG, "The date was reached...changed?");
        //return(df.format(new Date()));

        //doesn't work.
        return null;
    }

    public String getString(){
        String myString = "I come from the Service on another thread running in the background. hello World";
        return(myString);
    }

    public class MyLocalBinder extends Binder {
        MyService getService(){
            Log.i(TAG, "The binder was reached");
            return MyService.this;

        }
    }
}
