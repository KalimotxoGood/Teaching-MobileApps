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

public class MyService extends Service {
    private static final String TAG = "com.cwt59.myTest";
    private final IBinder caseysBinder = new MyLocalBinder();

    public MyService(){

    }

    @Override
    public IBinder onBind(Intent intent){
        Log.i(TAG, "The binder was returned!");
        return caseysBinder;
    }

    public String getCurrentTime(){
        SimpleDateFormat df = new SimpleDateFormat("HH:mm:ss, Local.US");
        Log.i(TAG, "The date was reached...changed?");
        return(df.format(new Date()));

    }

    public class MyLocalBinder extends Binder {
        MyService getService(){
            Log.i(TAG, "The binder was reached");
            return MyService.this;

        }
    }
}
