package com.example.cwt59.fastbreak;

import android.app.Service;
import android.content.Intent;
import android.os.IBinder;
import android.os.CountDownTimer;
import android.util.Log;

public class MyService extends Service {

    private final static String TAG = "MyService";

    public static final String COUNTDOWN_BR = "FastBreak.countdown_br";
    Intent bi = new Intent(COUNTDOWN_BR);

    CountDownTimer cdt = null;


    public void OnCreate(){
        super.onCreate();

        Log.i(TAG, "starting timer...");


        cdt = new CountDownTimer(30000,1000) {
            @Override
            public void onTick(long millisUntilFinished){
                Log.i(TAG, "Countdown seconds remaining: " +millisUntilFinished /1000);
                bi.putExtra("countdown", millisUntilFinished);
                sendBroadcast(bi);
            }

            @Override
            public void onFinish(){
                Log.i(TAG, "Timer finished");
            }
        };

        cdt.start();
    }
    @Override
    public void onDestroy() {

        cdt.cancel();
        Log.i(TAG, "Timer cancelled");
        super.onDestroy();
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        return super.onStartCommand(intent, flags, startId);
    }

    @Override
    public IBinder onBind(Intent arg0) {
        return null;
    }
}
