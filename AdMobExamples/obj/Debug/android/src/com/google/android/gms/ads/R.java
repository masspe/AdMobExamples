/* AUTO-GENERATED FILE.  DO NOT MODIFY.
 *
 * This class was automatically generated by the
 * aapt tool from the resource data it found.  It
 * should not be modified by hand.
 */

package com.google.android.gms.ads;

public final class R {
    public static final class attr {
        /** 
    The size of the ad. It must be one of BANNER, FULL_BANNER, LEADERBOARD,
    MEDIUM_RECTANGLE, SMART_BANNER, WIDE_SKYSCRAPER, FLUID or
    &lt;width&gt;x&lt;height&gt;.
    
         <p>Must be a string value, using '\\;' to escape characters such as '\\n' or '\\uxxxx' for a unicode character.
<p>This may also be a reference to a resource (in the form
"<code>@[<i>package</i>:]<i>type</i>:<i>name</i></code>") or
theme attribute (in the form
"<code>?[<i>package</i>:][<i>type</i>:]<i>name</i></code>")
containing a value of this type.
         */
        public static int adSize=0x7f010003;
        /** 
    A comma-separated list of the supported ad sizes. The sizes must be one of
    BANNER, FULL_BANNER, LEADERBOARD, MEDIUM_RECTANGLE, SMART_BANNER,
    WIDE_SKYSCRAPER, FLUID or &lt;width&gt;x&lt;height&gt;.
    
         <p>Must be a string value, using '\\;' to escape characters such as '\\n' or '\\uxxxx' for a unicode character.
<p>This may also be a reference to a resource (in the form
"<code>@[<i>package</i>:]<i>type</i>:<i>name</i></code>") or
theme attribute (in the form
"<code>?[<i>package</i>:][<i>type</i>:]<i>name</i></code>")
containing a value of this type.
         */
        public static int adSizes=0x7f010004;
        /**  The ad unit ID. 
         <p>Must be a string value, using '\\;' to escape characters such as '\\n' or '\\uxxxx' for a unicode character.
<p>This may also be a reference to a resource (in the form
"<code>@[<i>package</i>:]<i>type</i>:<i>name</i></code>") or
theme attribute (in the form
"<code>?[<i>package</i>:][<i>type</i>:]<i>name</i></code>")
containing a value of this type.
         */
        public static int adUnitId=0x7f010005;
        /** 
        Whether or not this view should have a circular clip applied
        
         <p>Must be a boolean value, either "<code>true</code>" or "<code>false</code>".
<p>This may also be a reference to a resource (in the form
"<code>@[<i>package</i>:]<i>type</i>:<i>name</i></code>") or
theme attribute (in the form
"<code>?[<i>package</i>:][<i>type</i>:]<i>name</i></code>")
containing a value of this type.
         */
        public static int circleCrop=0x7f010002;
        /** 
        The fixed aspect ratio to use in aspect ratio adjustments.
        
         <p>Must be a floating point value, such as "<code>1.2</code>".
<p>This may also be a reference to a resource (in the form
"<code>@[<i>package</i>:]<i>type</i>:<i>name</i></code>") or
theme attribute (in the form
"<code>?[<i>package</i>:][<i>type</i>:]<i>name</i></code>")
containing a value of this type.
         */
        public static int imageAspectRatio=0x7f010001;
        /** 
        What kind of aspect ratio adjustment to do.  It must be one of "none", "adjust_width",
        or "adjust_height".
        
         <p>Must be one of the following constant values.</p>
<table>
<colgroup align="left" />
<colgroup align="left" />
<colgroup align="left" />
<tr><th>Constant</th><th>Value</th><th>Description</th></tr>
<tr><td><code>none</code></td><td>0</td><td></td></tr>
<tr><td><code>adjust_width</code></td><td>1</td><td></td></tr>
<tr><td><code>adjust_height</code></td><td>2</td><td></td></tr>
</table>
         */
        public static int imageAspectRatioAdjust=0x7f010000;
    }
    public static final class drawable {
        public static int icon=0x7f020000;
        public static int splash=0x7f020001;
    }
    public static final class id {
        public static int adjust_height=0x7f060000;
        public static int adjust_width=0x7f060001;
        public static int none=0x7f060002;
    }
    public static final class integer {
        public static int google_play_services_version=0x7f040000;
    }
    public static final class string {
        public static int ApplicationName=0x7f03000a;
        public static int Hello=0x7f030009;
        public static int accept=0x7f030005;
        /**  Brand name for Facebook [DO NOT TRANSLATE] 
         */
        public static int auth_google_play_services_client_facebook_display_name=0x7f030001;
        /**  Brand name for Google [DO NOT TRANSLATE] 
         */
        public static int auth_google_play_services_client_google_display_name=0x7f030000;
        /**  Message in confirmation dialog informing user there is an unknown issue in Google Play
        services [CHAR LIMIT=NONE] 
         */
        public static int common_google_play_services_unknown_issue=0x7f030002;
        public static int create_calendar_message=0x7f030008;
        public static int create_calendar_title=0x7f030007;
        public static int decline=0x7f030006;
        public static int store_picture_message=0x7f030004;
        public static int store_picture_title=0x7f030003;
    }
    public static final class style {
        public static int Theme_IAPTheme=0x7f050000;
        public static int Theme_Splash=0x7f050001;
    }
    public static final class styleable {
        /** Attributes that can be used with a AdsAttrs.
           <p>Includes the following attributes:</p>
           <table>
           <colgroup align="left" />
           <colgroup align="left" />
           <tr><th>Attribute</th><th>Description</th></tr>
           <tr><td><code>{@link #AdsAttrs_adSize com.google.android.gms.ads:adSize}</code></td><td>
    The size of the ad.</td></tr>
           <tr><td><code>{@link #AdsAttrs_adSizes com.google.android.gms.ads:adSizes}</code></td><td>
    A comma-separated list of the supported ad sizes.</td></tr>
           <tr><td><code>{@link #AdsAttrs_adUnitId com.google.android.gms.ads:adUnitId}</code></td><td> The ad unit ID.</td></tr>
           </table>
           @see #AdsAttrs_adSize
           @see #AdsAttrs_adSizes
           @see #AdsAttrs_adUnitId
         */
        public static final int[] AdsAttrs = {
            0x7f010003, 0x7f010004, 0x7f010005
        };
        /**
          <p>
          @attr description
          
    The size of the ad. It must be one of BANNER, FULL_BANNER, LEADERBOARD,
    MEDIUM_RECTANGLE, SMART_BANNER, WIDE_SKYSCRAPER, FLUID or
    &lt;width&gt;x&lt;height&gt;.
    


          <p>Must be a string value, using '\\;' to escape characters such as '\\n' or '\\uxxxx' for a unicode character.
<p>This may also be a reference to a resource (in the form
"<code>@[<i>package</i>:]<i>type</i>:<i>name</i></code>") or
theme attribute (in the form
"<code>?[<i>package</i>:][<i>type</i>:]<i>name</i></code>")
containing a value of this type.
          <p>This is a private symbol.
          @attr name com.google.android.gms.ads:adSize
        */
        public static int AdsAttrs_adSize = 0;
        /**
          <p>
          @attr description
          
    A comma-separated list of the supported ad sizes. The sizes must be one of
    BANNER, FULL_BANNER, LEADERBOARD, MEDIUM_RECTANGLE, SMART_BANNER,
    WIDE_SKYSCRAPER, FLUID or &lt;width&gt;x&lt;height&gt;.
    


          <p>Must be a string value, using '\\;' to escape characters such as '\\n' or '\\uxxxx' for a unicode character.
<p>This may also be a reference to a resource (in the form
"<code>@[<i>package</i>:]<i>type</i>:<i>name</i></code>") or
theme attribute (in the form
"<code>?[<i>package</i>:][<i>type</i>:]<i>name</i></code>")
containing a value of this type.
          <p>This is a private symbol.
          @attr name com.google.android.gms.ads:adSizes
        */
        public static int AdsAttrs_adSizes = 1;
        /**
          <p>
          @attr description
           The ad unit ID. 


          <p>Must be a string value, using '\\;' to escape characters such as '\\n' or '\\uxxxx' for a unicode character.
<p>This may also be a reference to a resource (in the form
"<code>@[<i>package</i>:]<i>type</i>:<i>name</i></code>") or
theme attribute (in the form
"<code>?[<i>package</i>:][<i>type</i>:]<i>name</i></code>")
containing a value of this type.
          <p>This is a private symbol.
          @attr name com.google.android.gms.ads:adUnitId
        */
        public static int AdsAttrs_adUnitId = 2;
        /**  Attributes for LoadingImageView 
           <p>Includes the following attributes:</p>
           <table>
           <colgroup align="left" />
           <colgroup align="left" />
           <tr><th>Attribute</th><th>Description</th></tr>
           <tr><td><code>{@link #LoadingImageView_circleCrop com.google.android.gms.ads:circleCrop}</code></td><td>
        Whether or not this view should have a circular clip applied
        </td></tr>
           <tr><td><code>{@link #LoadingImageView_imageAspectRatio com.google.android.gms.ads:imageAspectRatio}</code></td><td>
        The fixed aspect ratio to use in aspect ratio adjustments.</td></tr>
           <tr><td><code>{@link #LoadingImageView_imageAspectRatioAdjust com.google.android.gms.ads:imageAspectRatioAdjust}</code></td><td>
        What kind of aspect ratio adjustment to do.</td></tr>
           </table>
           @see #LoadingImageView_circleCrop
           @see #LoadingImageView_imageAspectRatio
           @see #LoadingImageView_imageAspectRatioAdjust
         */
        public static final int[] LoadingImageView = {
            0x7f010000, 0x7f010001, 0x7f010002
        };
        /**
          <p>
          @attr description
          
        Whether or not this view should have a circular clip applied
        


          <p>Must be a boolean value, either "<code>true</code>" or "<code>false</code>".
<p>This may also be a reference to a resource (in the form
"<code>@[<i>package</i>:]<i>type</i>:<i>name</i></code>") or
theme attribute (in the form
"<code>?[<i>package</i>:][<i>type</i>:]<i>name</i></code>")
containing a value of this type.
          <p>This is a private symbol.
          @attr name com.google.android.gms.ads:circleCrop
        */
        public static int LoadingImageView_circleCrop = 2;
        /**
          <p>
          @attr description
          
        The fixed aspect ratio to use in aspect ratio adjustments.
        


          <p>Must be a floating point value, such as "<code>1.2</code>".
<p>This may also be a reference to a resource (in the form
"<code>@[<i>package</i>:]<i>type</i>:<i>name</i></code>") or
theme attribute (in the form
"<code>?[<i>package</i>:][<i>type</i>:]<i>name</i></code>")
containing a value of this type.
          <p>This is a private symbol.
          @attr name com.google.android.gms.ads:imageAspectRatio
        */
        public static int LoadingImageView_imageAspectRatio = 1;
        /**
          <p>
          @attr description
          
        What kind of aspect ratio adjustment to do.  It must be one of "none", "adjust_width",
        or "adjust_height".
        


          <p>Must be one of the following constant values.</p>
<table>
<colgroup align="left" />
<colgroup align="left" />
<colgroup align="left" />
<tr><th>Constant</th><th>Value</th><th>Description</th></tr>
<tr><td><code>none</code></td><td>0</td><td></td></tr>
<tr><td><code>adjust_width</code></td><td>1</td><td></td></tr>
<tr><td><code>adjust_height</code></td><td>2</td><td></td></tr>
</table>
          <p>This is a private symbol.
          @attr name com.google.android.gms.ads:imageAspectRatioAdjust
        */
        public static int LoadingImageView_imageAspectRatioAdjust = 0;
    };
}
