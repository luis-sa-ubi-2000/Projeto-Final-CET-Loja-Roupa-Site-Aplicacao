

package pt.atec.login;

import android.app.AlertDialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Build;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;


import com.google.common.util.concurrent.FutureCallback;
import com.google.common.util.concurrent.Futures;
import com.google.common.util.concurrent.ListenableFuture;
import com.google.common.util.concurrent.SettableFuture;
import com.microsoft.windowsazure.mobileservices.MobileServiceClient;
import com.microsoft.windowsazure.mobileservices.http.NextServiceFilterCallback;
import com.microsoft.windowsazure.mobileservices.http.OkHttpClientFactory;
import com.microsoft.windowsazure.mobileservices.http.ServiceFilter;
import com.microsoft.windowsazure.mobileservices.http.ServiceFilterRequest;
import com.microsoft.windowsazure.mobileservices.http.ServiceFilterResponse;
import com.microsoft.windowsazure.mobileservices.table.MobileServiceTable;
import com.microsoft.windowsazure.mobileservices.table.sync.MobileServiceSyncContext;
import com.microsoft.windowsazure.mobileservices.table.sync.localstore.ColumnDataType;
import com.microsoft.windowsazure.mobileservices.table.sync.localstore.MobileServiceLocalStoreException;
import com.microsoft.windowsazure.mobileservices.table.sync.localstore.SQLiteLocalStore;
import com.microsoft.windowsazure.mobileservices.table.sync.synchandler.SimpleSyncHandler;


import java.net.MalformedURLException;
import java.util.concurrent.TimeUnit;
import java.lang.String;


import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.ExecutionException;
import java.lang.Exception;

import okhttp3.OkHttpClient;


public class MainActivity extends AppCompatActivity {


    private EditText Utilizador;
    private EditText Password;
    private Button Entrar;
    private CheckBox Check;
    private int cont=5;
    private MobileServiceClient mClient;
    private ProgressBar Waiting;
    private MobileServiceTable<Pessoa> mToDoTable;
    private TextView Registar;
    private ProgressBar Waiting_Registo;




    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);


        try {
            // Create the client instance, using the provided mobile app URL.
            mClient = new MobileServiceClient(
                    "https://gaminglobby.azurewebsites.net/tables/Pessoa?ZUMO-API-VERSION=2.0.0",
                    this).withFilter(new ProgressFilter());

            Log.d("Luis","Entrei");

            // Extend timeout from default of 10s to 20s
            mClient.setAndroidHttpClientFactory(new OkHttpClientFactory() {
                @Override
                public OkHttpClient createOkHttpClient() {
                    OkHttpClient client = new OkHttpClient.Builder()
                            .connectTimeout(20, TimeUnit.SECONDS)
                            .readTimeout(20, TimeUnit.SECONDS)
                            .build();

                    return client;
                }
            });

            // Get the remote table instance to use.
            mToDoTable = mClient.getTable(Pessoa.class);



            //Init local storage
            initLocalStore().get();


        } catch (MalformedURLException e) {

            createAndShowDialog(new Exception("Erro ao criar a aplicação! Por favor verifique o URL."), "Erro");
        } catch (Exception e){
            createAndShowDialog(e, "Erro");
        }


        Utilizador = findViewById(R.id.user2);
        Password = findViewById(R.id.password2);
        Entrar = findViewById(R.id.entrar);
        Check = findViewById(R.id.checkBox);
        Waiting = findViewById(R.id.esperar_registo);
        Registar = findViewById(R.id.registo);
        Waiting_Registo = findViewById(R.id.esperar_registo);

        Waiting.setVisibility(View.INVISIBLE);

        Entrar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                validar(Utilizador.getText().toString(), Password.getText().toString());
                Waiting.setVisibility(ProgressBar.VISIBLE);
            }
        });

        Registar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(MainActivity.this,Registar.class);
                startActivity(intent);

            }
        });


    }


    //Ao clicar no botão Login

    private void validar(String user, String pass){


        if((user.equals(mToDoTable.where().field("nome"))) && (pass.equals(mToDoTable.where().field("senha")))){


            Intent intent = new Intent(MainActivity.this,Log_screen.class);
            startActivity(intent);

            boolean check = (Check).isChecked();

            if (check) {

                Password.setText(pass);

            } else {

                Password.setText("");

            }

        }else{

            cont--;

            Toast.makeText(this,"Tentativas restantes: " + cont,Toast.LENGTH_LONG).show();

            if(cont == 0) {

                Entrar.setEnabled(false);

            }
        }
    }

    private void createAndShowDialog(final String message, final String title) {
        final AlertDialog.Builder builder = new AlertDialog.Builder(this);

        builder.setMessage(message);
        builder.setTitle(title);
        builder.create().show();
    }

    private void createAndShowDialog(Exception exception, String title) {
        Throwable ex = exception;
        if(exception.getCause() != null){
            ex = exception.getCause();
        }
        createAndShowDialog(ex.getMessage(), title);
    }


    private void createAndShowDialogFromTask(final Exception exception, String title) {
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                createAndShowDialog(exception, "Erro");
            }
        });
    }




    private AsyncTask<Void, Void, Void> runAsyncTask(AsyncTask<Void, Void, Void> task) {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.HONEYCOMB) {
            return task.executeOnExecutor(AsyncTask.THREAD_POOL_EXECUTOR);
        } else {
            return task.execute();
        }
    }




    private AsyncTask<Void, Void, Void> initLocalStore() throws MobileServiceLocalStoreException, ExecutionException, InterruptedException {

        AsyncTask<Void, Void, Void> task = new AsyncTask<Void, Void, Void>() {
            @Override
            protected Void doInBackground(Void... params) {
                try {

                    MobileServiceSyncContext syncContext = mClient.getSyncContext();

                    if (syncContext.isInitialized())
                        return null;

                    SQLiteLocalStore localStore = new SQLiteLocalStore(mClient.getContext(), "OfflineStore", null, 1);

                    Map<String, ColumnDataType> tableDefinition = new HashMap<String, ColumnDataType>();
                    tableDefinition.put("id", ColumnDataType.String);
                    tableDefinition.put("nome", ColumnDataType.String);
                    tableDefinition.put("senha",ColumnDataType.String);
                    tableDefinition.put("complete", ColumnDataType.Boolean);

                    localStore.defineTable("Pessoa", tableDefinition);

                    SimpleSyncHandler handler = new SimpleSyncHandler();

                    syncContext.initialize(localStore, handler).get();

                } catch (final Exception e) {
                    createAndShowDialogFromTask(e, "Erro");
                }

                return null;
            }
        };

        return runAsyncTask(task);
    }


    private class ProgressFilter implements ServiceFilter {

        @Override
        public ListenableFuture<ServiceFilterResponse> handleRequest(ServiceFilterRequest request, NextServiceFilterCallback nextServiceFilterCallback) {

            final SettableFuture<ServiceFilterResponse> resultFuture = SettableFuture.create();


            runOnUiThread(new Runnable() {

                @Override
                public void run() {
                    if (Waiting != null) Waiting.setVisibility(ProgressBar.VISIBLE);
                }
            });

            ListenableFuture<ServiceFilterResponse> future = nextServiceFilterCallback.onNext(request);

            Futures.addCallback(future, new FutureCallback<ServiceFilterResponse>() {
                @Override
                public void onFailure(Throwable e) {
                    resultFuture.setException(e);
                }

                @Override
                public void onSuccess(ServiceFilterResponse response) {
                    runOnUiThread(new Runnable() {

                        @Override
                        public void run() {
                            if (Waiting != null) Waiting.setVisibility(ProgressBar.GONE);
                        }
                    });

                    resultFuture.set(response);
                }
            });

            return resultFuture;
        }
    }

/*
    private List<Pessoa> refreshItemsFromMobileServiceTable() throws ExecutionException, InterruptedException {
        return mToDoTable.where().field("complete").
                eq(val(false)).execute().get();
    }

*/



}
