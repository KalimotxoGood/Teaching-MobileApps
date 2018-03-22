package com.example.cwt59.fastbreak;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.IBinder;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.TextView;

public class WaterGauge extends AppCompatActivity {
    private static final String TAG = "com.cwt59.myTest";
    MyService caseysService;
    boolean isBound = false;
    String water = "waterwater water water water water water water water water water water water water water water ";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_water_gauge);

        //Binding to Service
        Intent i = new Intent(this,MyService.class);
        bindService(i, caseysConnection, Context.BIND_AUTO_CREATE);
        Log.i(TAG, "Water bind was reached");



    }

    public void showTime(View view){
        Log.i(TAG, "beginning of showTime was reached");
        //String currentTime = Boolean.toString(isBound);
        String currentTime = caseysService.getString();
        Log.i(TAG, "method called of getCurrentTime was reached");
        TextView tv = (TextView) findViewById(R.id.waterView);
        tv.setText(currentTime);
        Log.i(TAG, "end of showTime was reached");
    } //this function previously did not work because it was not existing when called on from the .xml file

    public void seeWater(View view){
        TextView tv = (TextView) findViewById(R.id.waterView);
        tv.setText(water);

    }

    public void drinkWater(View view){

    }






    private ServiceConnection caseysConnection = new ServiceConnection() {
        @Override
        public void onServiceConnected(ComponentName componentName, IBinder service) {
            MyService.MyLocalBinder binder = (MyService.MyLocalBinder) service;
            caseysService = binder.getService();
            Log.i(TAG, "Service connect reached!");
            isBound = true;

        }

        @Override
        public void onServiceDisconnected(ComponentName componentName) {
            isBound = false;
        }
    };

}
