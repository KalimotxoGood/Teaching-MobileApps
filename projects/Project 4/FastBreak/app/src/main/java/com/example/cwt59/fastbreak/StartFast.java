package com.example.cwt59.fastbreak;

import android.app.Service;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.nfc.Tag;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.util.Log;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.view.Menu;
import android.view.MenuItem;
import android.support.v7.app.ActionBar;
import android.os.IBinder;
import android.os.Binder;
import android.widget.Toast;
import android.content.ComponentName;
import android.content.ServiceConnection;
import com.example.cwt59.fastbreak.MyService;
import com.example.cwt59.fastbreak.MyService.MyLocalBinder;

public class StartFast extends AppCompatActivity {

    private static final String TAG = "com.cwt59.myTest";
    MyService caseysService;
    boolean isBound = false;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_start_fast);


    }

    public void startService(View view){


        EditText editText = (EditText) findViewById(R.id.hours);
        String value = editText.getText().toString();

        Intent serviceIntent = new Intent(this, MyService.class);
        serviceIntent.putExtra("Hours", value);
        this.startService(serviceIntent);
        bindService(serviceIntent, caseysConnection, Context.BIND_AUTO_CREATE);

        Log.i(TAG, "Started service");

        //  Because This button starts the service. I cannot get this activity to retrieve data from the
        //  service that it started. It leads to a crash when tried to do so.
        //


        //start another activity
        Intent intent = new Intent(this, Motivated.class);

        startActivity(intent);
    }

    //Start the Broadcast Receiver to get the time remaining
    private BroadcastReceiver br = new BroadcastReceiver() {


        @Override
        public void onReceive(Context context, Intent intent) {
            Toast.makeText(context, "Broadcast received", Toast.LENGTH_LONG).show();
            intent.getExtras();
            long millisUntilFinished = intent.getLongExtra("countdown", 0);
            Toast.makeText(context, Long.toString(millisUntilFinished), Toast.LENGTH_LONG).show();

        }
    };

    @Override
    public void onResume() {
        super.onResume();
        registerReceiver(br, new IntentFilter(MyService.COUNTDOWN_BR));
        Log.i(TAG, "Registered broacast receiver");
    }

    @Override
    public void onPause() {
        super.onPause();
        unregisterReceiver(br);
        Log.i(TAG, "Unregistered broacast receiver");
    }

    @Override
    public void onStop() {
        try {
            unregisterReceiver(br);
        } catch (Exception e) {
            // Receiver was probably already stopped in onPause()
        }
        super.onStop();
    }
    @Override
    public void onDestroy() {
        stopService(new Intent(this, MyService.class));
        Log.i(TAG, "Stopped service");
        super.onDestroy();
    }

    private void updateGUI(Intent intent) {

        if (intent.getExtras() != null) {
            long remainHours = intent.getLongExtra("countdown", 0);
            Log.i(TAG, "remaining Hours: " +  remainHours);

        }
        return ;
    }

    //Method to stop the service
    public void stopService(View view){
        //TextView tv = findViewById(R.id.timeView1);

        stopService(new Intent(getBaseContext(), MyService.class));

    }


    private ServiceConnection caseysConnection = new ServiceConnection() {
        @Override
        public void onServiceConnected(ComponentName componentName, IBinder service) {
            MyLocalBinder binder = (MyLocalBinder) service;
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

