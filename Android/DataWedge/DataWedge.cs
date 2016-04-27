using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;

namespace DataWedge
{
    public static class cDataWedge
    {
        // Let's define some intent strings
        // This intent string contains the source of the data as a string
        public static readonly String SOURCE_TAG = "com.motorolasolutions.emdk.datawedge.source";
        // This intent string contains the barcode symbology as a string
        public static readonly String LABEL_TYPE_TAG = "com.motorolasolutions.emdk.datawedge.label_type";
        // This intent string contains the barcode data as a byte array list
        public static readonly String DECODE_DATA_TAG = "com.motorolasolutions.emdk.datawedge.decode_data";

        // This intent string contains the captured data as a string
        // (in the case of MSR this data string contains a concatenation of the track data)
        public static readonly String DATA_STRING_TAG = "com.motorolasolutions.emdk.datawedge.data_string";

        // Let's define the MSR intent strings (in case we want to use these in the future)
        public static readonly String MSR_DATA_TAG = "com.motorolasolutions.emdk.datawedge.msr_data";
        public static readonly String MSR_TRACK1_TAG = "com.motorolasolutions.emdk.datawedge.msr_track1";
        public static readonly String MSR_TRACK2_TAG = "com.motorolasolutions.emdk.datawedge.msr_track2";
        public static readonly String MSR_TRACK3_TAG = "com.motorolasolutions.emdk.datawedge.msr_track3";
        public static readonly String MSR_TRACK1_STATUS_TAG = "com.motorolasolutions.emdk.datawedge.msr_track1_status";
        public static readonly String MSR_TRACK2_STATUS_TAG = "com.motorolasolutions.emdk.datawedge.msr_track2_status";
        public static readonly String MSR_TRACK3_STATUS_TAG = "com.motorolasolutions.emdk.datawedge.msr_track3_status";
        public static readonly String MSR_TRACK1_ENCRYPTED_TAG = "com.motorolasolutions.emdk.datawedge.msr_track1_encrypted";
        public static readonly String MSR_TRACK2_ENCRYPTED_TAG = "com.motorolasolutions.emdk.datawedge.msr_track2_encrypted";
        public static readonly String MSR_TRACK3_ENCRYPTED_TAG = "com.motorolasolutions.emdk.datawedge.msr_track3_encrypted";
        public static readonly String MSR_TRACK1_HASHED_TAG = "com.motorolasolutions.emdk.datawedge.msr_track1_hashed";
        public static readonly String MSR_TRACK2_HASHED_TAG = "com.motorolasolutions.emdk.datawedge.msr_track2_hashed";
        public static readonly String MSR_TRACK3_HASHED_TAG = "com.motorolasolutions.emdk.datawedge.msr_track3_hashed";

        // Let's define the API intent strings for the soft scan trigger
        public static readonly String ACTION_SOFTSCANTRIGGER = "com.motorolasolutions.emdk.datawedge.api.ACTION_SOFTSCANTRIGGER";
        public static readonly String EXTRA_PARAM = "com.motorolasolutions.emdk.datawedge.api.EXTRA_PARAMETER";
        public static readonly String DWAPI_START_SCANNING = "START_SCANNING";
        public static readonly String DWAPI_STOP_SCANNING = "STOP_SCANNING";
        public static readonly String DWAPI_TOGGLE_SCANNING = "TOGGLE_SCANNING";

        public static string HandleDecodeData(Intent i)
        {
            // define a string that will hold our output
            String outmsg = "";
            // get the source of the data
            String source = i.GetStringExtra(SOURCE_TAG);
            // save it to use later
            if (source == null) source = "scanner";
            // get the data from the intent
            String data = i.GetStringExtra(DATA_STRING_TAG);
            // let's define a variable for the data length
            int data_len = 0;
            // and set it to the length of the data
            if (data != null) data_len = data.Length;

            // check if the data has come from the barcode scanner
            if (source.Equals("scanner", StringComparison.InvariantCultureIgnoreCase))
            {
                // check if there is anything in the data
                if (data != null && data.Length > 0)
                {
                    // we have some data, so let's get it's symbology
                    String sLabelType = i.GetStringExtra(LABEL_TYPE_TAG);
                    // check if the string is empty
                    if (sLabelType != null && sLabelType.Length > 0)
                    {
                        // format of the label type string is LABEL-TYPE-SYMBOLOGY
                        // so let's skip the LABEL-TYPE- portion to get just the symbology
                        sLabelType = sLabelType.Substring(11);
                    }
                    else
                    {
                        // the string was empty so let's set it to "Unknown"
                        sLabelType = "Unknown";
                    }
                    // let's construct the beginning of our output string
                    outmsg =  data + "|Scanner|" + sLabelType + "|"+data_len.ToString();
                }
            }

            // check if the data has come from the MSR
            if (source.Equals("msr", StringComparison.InvariantCultureIgnoreCase))
            {
                // construct the beginning of our output string
                outmsg = outmsg = data + "|MSR||" + data_len.ToString();
            }
            return outmsg;
            //// let's get our edit box view
            //EditText et = (EditText)findViewById(R.id.editbox);
            //// and get it's text into an editable string
            //Editable txt = et.getText();
            //// now because we want format our output
            //// we need to put the edit box text into a spannable string builder
            //SpannableStringBuilder stringbuilder = new SpannableStringBuilder(txt);
            //// add the output string we constructed earlier
            //stringbuilder.append(out);
            //// now let's highlight our output string in bold type
            //stringbuilder.setSpan(new StyleSpan(Typeface.BOLD), txt.length(), stringbuilder.length(), SpannableString.SPAN_EXCLUSIVE_EXCLUSIVE);
            //// then add the barcode or msr data, plus a new line, and add it to the string builder
            //stringbuilder.append(data + "\r\n");
            //// now let's update the text in the edit box
            //et.setText(stringbuilder);
            //// we want the text cursor to be at the end of the edit box
            //// so let's get the edit box text again
            //txt = et.getText();
            //// and set the cursor position at the end of the text
            //et.setSelection(txt.length());
            //// and we are done!
        }

    }

}

